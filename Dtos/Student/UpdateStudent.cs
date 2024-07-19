using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProposalSystem.Dtos.Student
{
    public class UpdateStudent
    {
        [Required]
        public string? UserName { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        
    }
}