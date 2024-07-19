using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProposalSystem.Dtos.Proposal
{
    public class UpdateMark
    {
        [Column(TypeName = "decimal(4,2)")]
        [Range(0,100)]
        public decimal? Mark {get; set;}
        
        [Required]
        public string? EvaluatorId {get; set;}
    }
}