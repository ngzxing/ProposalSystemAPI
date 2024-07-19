using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProposalSystem.Data.Enum;

namespace ProposalSystem.Dtos.Committee
{
    public class GetCommitteeResult
    {
        public string? Id {get; set;}
        public string? LecturerId {get; set;}
        public string? LecturerName {get; set;}
        public Domain? LecturerDomain {get; set;}
        public string? AcademicProgramId {get; set;}
        public string? AcademicProgramName {get; set;}
    }
}