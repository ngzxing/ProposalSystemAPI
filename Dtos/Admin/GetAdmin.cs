using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProposalSystem.Dtos.Admin
{
    public class GetAdmin
    {
        public string? Id {get; set;}
        public string? AdminId {get; set;}
        public string? UserName {get; set;}
        public string? Email {get; set;}
    }
}