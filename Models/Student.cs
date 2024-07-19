using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ProposalSystem.Models
{
    
    public class Student
    {
        [Key]
        [ForeignKey("AppUser")]
        public string Id {get; set;}

        public AppUser? AppUser {get; set;}

        [Required]
        public string? MatricId {get; set;}

        [Required]
        public int? Year {get; set;}

        [Required]
        public int? Session {get; set;}

        [Required]
        public int? Semester {get; set;}

        [Required]
        [ForeignKey("AcademicProgram")]
        public string? AcademicProgramId {get; set;}

        public AcademicProgram? AcademicProgram {get; set;}


        public string? SupervisorId {get; set;}
        public Lecturer? Supervisor {get; set;}


        public string? FirstEvaluatorId {get; set;}
        public Lecturer? FirstEvaluator {get; set;}

    
        public string? SecondEvaluatorId {get; set;}
        public Lecturer? SecondEvaluator {get; set;}

        public ICollection<Proposal>? Proposals {get; set;}

        public ICollection<ApplySupervisor>? Applications {get; set;}

    }
}