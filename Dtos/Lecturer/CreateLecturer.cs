using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ProposalSystem.Data.Enum;
using ProposalSystem.Models;

namespace ProposalSystem.Dtos.Lecturer
{
    public class CreateLecturer
    {
        [Required]
        required public string UserName { get; set; }

        [Required]
        required public string StaffId {get; set;}

        [Required]
        [Phone]
        required public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        required public string Email { get; set; }

        [Required]
        [PasswordPropertyText]
        required public string Password { get; set; }

        [Required]
        required public Domain Domain {get; set;}

        [Required]
        public string? AcademicProgramId {get; set;}


    }
}