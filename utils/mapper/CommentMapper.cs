using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProposalSystem.Dtos.Comment;
using ProposalSystem.Models;

namespace ProposalSystem.utils.mapper
{
    public class CommentMapper : Profile
    {
        public CommentMapper(){

            CreateMap< CreateComment, Comment >()
                    .ForMember(
                        dest => dest.BackupEvaluatorId,
                        opt => opt.MapFrom( src => src.EvaluatorId )
                    );

            CreateMap<Comment, GetCommentResult>()
                .ForMember(
                    
                    dest => dest.EvaluatorId,
                    opt => opt.MapFrom( src => src.BackupEvaluatorId )

                );
        }
    }
}