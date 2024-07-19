using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProposalSystem.Dtos.Student
{
    public class CreateStudent
    {
        [Required]
        required public string UserName { get; set; }
        
        [Required]
        [Phone]
        required public string PhoneNumber { get; set; }

        [Required]
        required public string MatricId { get; set; }

        [Required]
        [EmailAddress]
        required public string Email { get; set; }

        [Required]
        [PasswordPropertyText]
        required public string Password { get; set; }

        [Required]
        required public int Year {get; set;}

        [Required]
        required public int Session {get; set;}

        [Required]
        required public int Semester {get; set;}

        [Required]
        public string? AcademicProgramId {get; set;}

    }
}