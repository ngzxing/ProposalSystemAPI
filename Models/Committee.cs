using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProposalSystem.Models
{
    public class Committee
    {   
        [Key]
        public string? Id {get; set;}
        
        [Required]
        public string? LecturerId {get; set;}
        public Lecturer? Lecturer {get; set;}


        [Required]
        [ForeignKey("AcademicProgram")]
        public string? AcademicProgramId {get; set;}
        public AcademicProgram? AcademicProgram {get; set;}

    }
}