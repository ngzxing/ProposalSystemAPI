using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProposalSystem.Dtos.Student
{
    public class UpdateSupervisor
    {   
        [Required]
        public string? SupervisorId {get; set;}
    }
}