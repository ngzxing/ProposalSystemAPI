using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProposalSystem.utils.helpers
{
    public class GoogleDriveSettings
    
    {
        required public string AccessToken {get; set;}
        required public string RefreshToken {get; set;}
        required public string UserName {get; set;}
        required public string ClientId {get; set;}
        required public string ClientSecret {get; set;}
        required public string ApplicationName {get; set;}
    }
}