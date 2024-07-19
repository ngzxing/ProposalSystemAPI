using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProposalSystem.Models
{
    public class Comment
    {
        [Key]
        public string? Id {get; set;}
        
        [Required]
        [ForeignKey("Proposal")]
        public string? ProposalId {get; set;}
        public Proposal? Proposal {get; set;}

        [Required]
        public string? EvaluatorId {get; set;}
        public Lecturer? Evaluator {get; set;}

        [Required]
        public string? BackupEvaluatorId {get; set;}

        [Required]
        public string? Text {get; set;}

        [Required]
        public DateTime CreatedAt { get; set; } = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time"));

    }
}