using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProposalSystem.Data.Enum;
using ProposalSystem.Dtos.Comment;
using ProposalSystem.Models;

namespace ProposalSystem.Dtos.Proposal
{
    public class GetProposalResult
    {
    
        public string? Id {get; set;}
        public string? StudentId  {get; set;}
        public string? StudentName {get; set;}

        public string? AcademicProgramName {get; set;}
        public string? FirstEvaluatorId {get; set;}
        public string? FirstEvaluatorName {get; set;}
        public string? SecondEvaluatorId {get; set;}
        public string? SecondEvaluatorName {get; set;}
        public string? SupervisorId {get; set;}
        public string? SupervisorName {get; set;}
        public int? Year {get; set; }
        public int? Session {get; set;}
        public int? Semester  {get; set;}
        public ProposalStatus? ProposalStatus {get; set;}
        public decimal? Mark {get; set;}
        public string? Title {get; set;}
        public Domain? Domain {get; set;}
        public DateTime CreatedAt { get; set; }
        public IEnumerable<GetCommentResult>? Comments {get; set;}
    }
}