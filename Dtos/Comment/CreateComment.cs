using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProposalSystem.Dtos.Comment
{
    public class CreateComment
    {
        public string? ProposalId {get; set;}
        public string? EvaluatorId {get; set;}
        public string? Text {get; set;}
    }
}