using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ProposalSystem.Data.Enum;

namespace ProposalSystem.Models
{
    public class ApplySupervisor
    {   
        [Key]
        public string Id {get; set;}

        [Required]
        public string? SupervisorId {get; set;}
        public Lecturer? Supervisor {get; set;}

        [Required]
        public string? MatricId {get; set;}
        public Student? Student {get; set;}
        
        [Required]
        public ApplySupervisorStatus? ApplyState;
    }
}