using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProposalSystem.Dtos.Committee
{
    public class CreateCommittee
    {
        public string? AcademicProgramId {get; set;}
        public string? LecturerId {get; set;}
    }
}