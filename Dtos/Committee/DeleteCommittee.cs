using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProposalSystem.Dtos.Committee
{
    public class DeleteCommittee
    {   
        [Required]
        public string? AcademicProgramId;
    }
}