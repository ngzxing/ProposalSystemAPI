using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudinaryDotNet.Actions;

namespace ProposalSystem.Interfaces
{
    public interface ICloudService
    {
        Task<(string?, string?)> AddPDFAsync(IFormFile file);

        Task<string?> DeletePDFAsync(string publicId);

        Task<(MemoryStream, string?)> GetPDFByIdAsync(string publicId);
    }
}