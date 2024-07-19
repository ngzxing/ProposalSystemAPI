using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProposalSystem.Data;
using ProposalSystem.Dtos.Comment;
using ProposalSystem.Interfaces;
using ProposalSystem.Models;

namespace ProposalSystem.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public CommentRepository( IMapper mapper, ApplicationDbContext context ){

            _mapper = mapper;
            _context = context;
        }
        
        public async Task<(GetCommentResult?, string?)> CreateAsync(CreateComment dto)
        {

            Comment comment = _mapper.Map<Comment>( dto );
            
            await _context.Comments.AddAsync( comment );

            var result = await _context.SaveChangesAsync();

            if(result > 0){

                var response = _mapper.Map<GetCommentResult>( comment );
                response.EvaluatorName = await _context.Lecturers.Where( l => l.StaffId == dto.EvaluatorId ).Select( l => l.AppUser.UserName ).FirstOrDefaultAsync();

                return ( response, null  );
            }
            
            
            return (null, "Save Change Failed");
        }

        public async Task<string?> DeleteCommentAsync(string id)
        {
            Comment comment = new Comment{ Id = id };
            _context.Remove(comment);

            var result = await _context.SaveChangesAsync();

            if( result > 0 )
                return null;

            return "Save Change Failed";

        }

        public async Task<GetCommentResult?> GetByIdAsync(string CommentId)
        {
            Comment? comment = await _context.Comments.AsNoTracking().FirstOrDefaultAsync( c => c.Id == CommentId );


            if( comment == null )
                return null;

            var response = _mapper.Map<GetCommentResult>( comment );

            await _context.Lecturers.Where( l => l.StaffId == comment.EvaluatorId ).Select( l => l.AppUser.UserName ).FirstOrDefaultAsync();
            response.EvaluatorName = await _context.Lecturers.Where( l => l.StaffId == comment.EvaluatorId ).Select( l => l.AppUser.UserName ).FirstOrDefaultAsync();

            return response;
        }

        public async Task<IEnumerable<GetCommentResult>?> GetByProposalIdAsync(string ProposalId)
        {
            var comments = await _context.Comments.Where( c => c.ProposalId == ProposalId ).AsNoTracking().ToListAsync();

            var result = comments.Select ( _mapper.Map<GetCommentResult> ).ToList();
            
            for( int i = 0; i < result.Count(); i++ ){

                result[i].EvaluatorName = await _context.Lecturers.Where( l => l.StaffId == result[i].EvaluatorId ).Select( l => l.AppUser.UserName ).FirstOrDefaultAsync();
            }

            
            return result.OrderByDescending(e => e.CreatedAt ).ToList();
        }
    }
}