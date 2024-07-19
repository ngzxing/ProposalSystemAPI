using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProposalSystem.Dtos.Comment
{
    public class GetCommentResult
    {   
        public string? Id {get; set;}
        public string? EvaluatorId {get; set;}
        public string? EvaluatorName {get; set;}
        public DateTime? CreatedAt {get; set;}
        public string? Text {get; set;}
    }
}