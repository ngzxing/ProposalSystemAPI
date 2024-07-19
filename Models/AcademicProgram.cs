using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProposalSystem.Models
{
    public class AcademicProgram
    {   
        [Key]
        public string? Id {get; set;}

        [Required]
        public string? Name {get; set;}

        [Required]
        public string? Description {get; set;}

        public ICollection<Committee>? Committees {get; set;}
    }
}