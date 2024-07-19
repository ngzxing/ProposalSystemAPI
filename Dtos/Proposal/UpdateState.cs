using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ProposalSystem.Data.Enum;

namespace ProposalSystem.Dtos.Proposal
{
    public class UpdateState
    {   
        [Required]
        public ProposalStatus? ProposalStatus {get; set;}

        [Required]
        public string? EvaluatorId {get; set;}
    }
}