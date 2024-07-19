using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProposalSystem.Dtos
{
    public class QueryStudent
    {
        public string? UserName {get; set;}

        [Phone]
        public string? PhoneNumber { get; set; }

        [EmailAddress]
        public string? Email {get; set;}
        public int? Year {get; set;}

        public int? Session {get; set;}

        public int? Semester {get; set;}

        public string? SupervisorId {get; set;}
        public string? SupervisorName {get; set;}

        public string? EvaluatorId {get; set;}

        public string? EvaluatorName {get; set;}
        public string? CommitteeLecturerId {get; set;}

    }
}