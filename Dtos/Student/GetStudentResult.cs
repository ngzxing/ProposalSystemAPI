using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProposalSystem.Dtos.Student
{
    public class GetStudentResult
    {
        public string UserName { get; set; }

        [Required]
        [Phone]
        required public string PhoneNumber { get; set; }

        [Required]
        required public string MatricId { get; set; }

        [EmailAddress]
        public string Email { get; set;}
        public int? Year {get; set;}

        public int? Session {get; set;}

        public int? Semester {get; set;}

        public string? SupervisorId {get; set;}
        public string? SupervisorName {get; set;}

        public string? FirstEvaluatorId {get; set;}
        public string? FirstEvaluatorName {get; set;}

        public string? SecondEvaluatorId {get; set;}
        public string? SecondEvaluatorName {get; set;}
        public string? AcademicProgramId {get; set;}

        public string? AcademicProgramName {get; set;}


    }
}