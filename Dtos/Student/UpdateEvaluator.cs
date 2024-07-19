using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ProposalSystem.Data.Enum;

namespace ProposalSystem.Dtos.Student
{
    public class UpdateEvaluator
    {   
        [Required]
        public string? EvaluatorId { get; set; }

        [Required]
        [Range(0,1)]
        public int? who { get; set; }
        public Domain? Domain { get; set; }
    }
}