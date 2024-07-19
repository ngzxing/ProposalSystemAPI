using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ProposalSystem.Data.Enum;

namespace ProposalSystem.Models
{
    public class Proposal
    {   
        [Key]
        public string? Id {get; set;}
        [Required]
        public string? StudentId  {get; set;}
        public Student? Student {get; set;}

        [Required]
        public int? Year {get; set; }

        [Required]
        public int? Session {get; set;}
        
        [Required]
        public int? Semester  {get; set;}

        [Required]
        public string? LinkProposal  {get; set;}

        [Required]
        public string? LinkForm  {get; set;}

        [Required]
        public ProposalStatus? ProposalStatus {get; set;}

        [Column(TypeName = "decimal(4,2)")]
        [Range(0,100)]
        public decimal? Mark {get; set;}

        [Required]
        public string? Title {get; set;}

        [Required]
        public Domain? Domain {get; set;}

        [Required]
        public DateTime CreatedAt { get; set; } = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time"));

        public ICollection<Comment>? Comments {get; set;}

    }


}