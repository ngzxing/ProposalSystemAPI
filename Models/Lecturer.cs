using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ProposalSystem.Data.Enum;

namespace ProposalSystem.Models
{
    public class Lecturer
    {
        [Key]
        [ForeignKey("AppUser")]
        public string Id {get; set;}
        public AppUser? AppUser {get; set;}

        [Required]
        public string? StaffId {get; set;}

        [Required]
        [ForeignKey("AcademicProgram")]
        public string? AcademicProgramId {get; set;}

        public AcademicProgram? AcademicProgram {get; set;}

        public Domain? Domain {get; set;}

        public ICollection<Student>? SupervisedStudents {get; set;}

        public ICollection<Student>? FirstEvaluatedStudents {get; set;}

        public ICollection<Student>? SecondEvaluatedStudents {get; set;}

        public ICollection<Comment>? Comments {get; set;}

        public ICollection<Committee>? Committee {get; set;}

        public ICollection<ApplySupervisor>? Applications {get; set;}

    }
}