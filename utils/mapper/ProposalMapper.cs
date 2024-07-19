using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProposalSystem.Dtos.Proposal;
using ProposalSystem.Dtos.Student;
using ProposalSystem.Models;

namespace ProposalSystem.utils.mapper
{
    public class ProposalMapper : Profile
    {
        public ProposalMapper(){

            CreateMap<GetStudentResult, GetProposalResult>()
                .ForMember(
                    dest => dest.StudentName,
                    opt  => opt.MapFrom( src => src.UserName )
                );
            CreateMap<Proposal, GetProposalResult>();
        }
    }
}