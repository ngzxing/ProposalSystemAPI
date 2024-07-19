using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ProposalSystem.Models
{
    public class AppUser : IdentityUser
    {   
        [Required]
        public bool Logined {get; set;} = false;
    }
}