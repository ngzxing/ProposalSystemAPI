using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ProposalSystem.Data.Enum;

namespace ProposalSystem.Dtos.Lecturer
{
    public class UpdateLecturer
    {
        [Required]
        required public string UserName { get; set; }

        [Required]
        [Phone]
        required public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        required public string Email { get; set;}

        [Required]
        required public Domain? Domain {get; set;}
    }
}