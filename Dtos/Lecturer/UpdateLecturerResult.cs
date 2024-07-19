using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ProposalSystem.Data.Enum;

namespace ProposalSystem.Dtos.Lecturer
{
    public class UpdateLecturerResult
    {

        public string? StaffId { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }

        public string? UserName { get; set; }
        
        [EmailAddress]
        public string? Email { get; set; }

        public Domain? Domain {get; set;}

    }
}