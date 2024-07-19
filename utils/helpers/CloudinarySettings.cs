using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProposalSystem.utils.helpers
{
    public class CloudinarySettings
    {
        required public string CloudName {get; set;}
        required public string ApiKey {get; set;}
        required public string ApiSecret {get; set;}
    }
}