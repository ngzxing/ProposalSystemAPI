using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProposalSystem.Dtos.AcademicProgram;
using ProposalSystem.Models;

namespace ProposalSystem.utils.mapper
{
    public class AcademicProgramMapper : Profile
    {
        public AcademicProgramMapper(){

            CreateMap< CreateAcademicProgram, AcademicProgram >();
        }
        
    }
}