using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProposalSystem.Data.Enum;

namespace ProposalSystem.Dtos.ApplySupervisor
{
    public class GetApplyResult
    {
        public string? Id {get; set;}
        public string? MatricId {get; set;}
        public string? SupervisorId {get; set;}
        public string? StudentName {get; set;}
        public int? Year {get; set;}
        public int? Semester {get; set;}
        public int? Session {get; set;}
        public Domain? Domain {get; set;}
        public string? SupervisorName {get; set;}
        public ApplySupervisorStatus? status {get; set;}
        
    }
}