using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using ProposalSystem.Interfaces;
using ProposalSystem.utils.helpers;
using Google.Apis.Discovery.v1;
using Google.Apis.Discovery.v1.Data;
using Google.Apis.Services;
using Google.Apis.Drive.v3;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Util.Store;
using System.Collections;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Auth.OAuth2.Flows;
using static Google.Apis.Drive.v3.DriveService;
using Google.Apis.Download;
using Google;


namespace ProposalSystem.Services
{
    public class CloudPDFService : ICloudService
    {   
        private readonly DriveService _service;

        public CloudPDFService(IOptions<GoogleDriveSettings> config)
        {
            var tokenResponse = new TokenResponse
            {
                AccessToken = config.Value.AccessToken ,
                RefreshToken = config.Value.RefreshToken
            };
            
            var applicationName = config.Value.ApplicationName;
            var username = config.Value.UserName;
            
            var apiCodeFlow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = config.Value.ClientId,
                    ClientSecret = config.Value.ClientSecret
                },
                Scopes = new[] { Scope.Drive },
                DataStore = new FileDataStore(applicationName)
            });
    
            var credential = new UserCredential(apiCodeFlow, username, tokenResponse);

            _service = new DriveService(new BaseClientService.Initializer
            {
                    HttpClientInitializer = credential,
                    ApplicationName = config.Value.ApplicationName
            });
        }


        
        public async Task<(string?, string?)> AddPDFAsync(IFormFile file)
        {
            try{

                var fileMetadata = new Google.Apis.Drive.v3.Data.File()
                {
                    Name = Path.GetFileName(file.FileName),
                    Parents = new List<string> { "1BkuSjQ4vlWDuK1s-oHsano3A6bw8B6bQ" }
                };

                FilesResource.CreateMediaUpload request;
                Google.Apis.Upload.IUploadProgress progress;
                using (var stream = file.OpenReadStream())
                {
                    request = _service.Files.Create(fileMetadata, stream, "application/pdf");
                    request.Fields = "id";
                    progress = await request.UploadAsync();
                }

                
                if (progress.Exception != null)
                {
                    return (null, progress.Exception.ToString());
                }

                var fileResponse = request.ResponseBody;
            
                Console.WriteLine("File ID: " + fileResponse.Id);
                return (fileResponse.Id, null);

            }catch(GoogleApiException e){

                return (null, "Add File Error: " + e.ToString());

            }catch(Exception e){

                return (null, "System Error: " + e.ToString());
            }

                

        }

        public async Task<string?> DeletePDFAsync(string fileId)
        {
            try{

                var request = _service.Files.Delete(fileId);
                var result = await request.ExecuteAsync();

                return null;

            }catch(GoogleApiException e){

                return "Google Drive Error: " + e.ToString();

            }catch(Exception e){

                return "System Error: " + e.ToString();
            }


        }

        public async Task<(MemoryStream?, string?)> GetPDFByIdAsync(string fileId){

            try{
                
                var request = _service.Files.Get(fileId);
                var stream = new MemoryStream();
                bool success = true;

                request.MediaDownloader.ProgressChanged +=
                        progress =>
                        {
                            switch (progress.Status)
                            {
                                case DownloadStatus.Completed:
                                {
                                    Console.WriteLine("Download complete.");
                                    break;
                                }
                                case DownloadStatus.Failed:
                                {
                                    Console.WriteLine("Download failed.");
                                    success = false;
                                    break;
                                }
                            }
                        };

                await request.DownloadAsync(stream);

                if (!success)
                    return (null,"Download Failed For Unknown Error");
                
                return (stream, null);
                
            }
            catch(GoogleApiException e){

                return (null, "Google Drive Error: " + e.ToString());
            }catch(Exception e){

                return (null, "System Error: " + e.ToString());
            }
            
        }
    }
}