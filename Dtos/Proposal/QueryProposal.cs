using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ProposalSystem.Data.Enum;
using ProposalSystem.Models;

namespace ProposalSystem.Dtos.Proposal
{
    public class QueryProposal
    {
        public string? StudentId  {get; set;}
        public string? StudentName {get; set;}
        public string? EvaluatorId {get; set;}
        public string? EvaluatorName {get; set;}
        public string? SupervisorId {get; set;}
        public string? SupervisorName {get; set;}
        public int? Year {get; set; }
        public int? Session {get; set;}
        public int? Semester  {get; set;}
        public ProposalStatus? ProposalStatus {get; set;}
        public decimal? MarkLower {get; set;}
        public decimal? MarkEqual {get; set;}
        public decimal? MarkGreater {get; set;}
        public string? Title {get; set;}
        public Domain? Domain {get; set;}
        public DateTime? CreatedAtLower { get; set; }
        public DateTime? CreatedAtGreater { get; set; }

        public string? CommitteeLecturerId {get; set;}

        
    }
}