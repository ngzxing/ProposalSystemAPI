using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ProposalSystem.Dtos.Lecturer;

namespace ProposalSystem.Dtos.AcademicProgram
{
    public class GetAcademicProgram
    {
        [Key]
        public string? Id {get; set;}

        [Required]
        public string? Name {get; set;}

        [Required]
        public string? Description {get; set;}

        public ICollection<GetLecturerResult>? Committees {get; set;}
    }
}