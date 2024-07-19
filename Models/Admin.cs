using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProposalSystem.Models
{
    public class Admin
    {
        [ForeignKey("AppUser")]
        [Required]
        public string? Id {get; set;}

        [Required]
        public string? AdminId {get; set;}

        public AppUser? AppUser {get; set;}
    }
}