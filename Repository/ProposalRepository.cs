using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProposalSystem.Data;
using ProposalSystem.Data.Enum;
using ProposalSystem.Dtos.Comment;
using ProposalSystem.Dtos.Proposal;
using ProposalSystem.Dtos.Student;
using ProposalSystem.Interfaces;
using ProposalSystem.Models;

namespace ProposalSystem.Repository
{
    public class ProposalRepository : IProposalRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProposalRepository(ApplicationDbContext context, IMapper mapper){

            _context = context;
            _mapper = mapper;
        }
        
        public async Task<(GetProposalResult?, string?)> CreateAsync(CreateProposal dto, string proposalLink, string formLink, GetStudentResult student)
        {
            var newProposal = new Proposal{

                StudentId = student.MatricId,
                Year = student.Year,
                Session = student.Session,
                Semester = student.Semester,
                LinkProposal = proposalLink,
                LinkForm = formLink,
                ProposalStatus = ProposalStatus.Pending,
                Title = dto.Title,
                Domain = dto.Domain
            };

            await _context.Proposals.AddAsync(newProposal);
            var result = await _context.SaveChangesAsync();

            GetProposalResult proposal = _mapper.Map<GetProposalResult>(newProposal);
            _mapper.Map<GetStudentResult, GetProposalResult>(student, proposal);


            if (result > 0)
                return (proposal, null);
            
            return (null, "Save Proposal Failed For Unknown Reason");
        }

        public async Task<string?> DeleteAsync(string ProposalId)
        {
            Proposal? proposal = await _context.Proposals.Include( p => p.Comments ).FirstOrDefaultAsync( p => p.Id == ProposalId );

            if (proposal == null)
                return "Proposal Not Found";
            
            _context.Remove( proposal );
            var result = await _context.SaveChangesAsync();

            if(result > 0)
                return null;
            
            return "Fail to Save Change";
        }

        public async Task< ( ICollection<GetProposalResult>?, string? ) > GetAllInfoAsync(QueryProposal dto)
        {
            
            var proposals = _context.Proposals.AsQueryable();

            if( !string.IsNullOrWhiteSpace(dto.StudentId) )
                proposals = proposals.Where( p => p.StudentId == dto.StudentId );

            if( !string.IsNullOrWhiteSpace(dto.StudentName) )
                proposals = proposals.Where( p => p.Student.AppUser.UserName.Contains( dto.StudentName ) );

            if( !string.IsNullOrWhiteSpace(dto.EvaluatorId) )
                proposals = proposals.Where( p => (p.Student.FirstEvaluatorId == dto.EvaluatorId ) || ( p.Student.SecondEvaluatorId ==  dto.EvaluatorId   ));

            if( !string.IsNullOrWhiteSpace(dto.EvaluatorName) )
                proposals = proposals.Where( p => ( p.Student.FirstEvaluator.AppUser.UserName.Contains( dto.EvaluatorName ) ) || ( p.Student.SecondEvaluator.AppUser.UserName.Contains( dto.EvaluatorName ) )  );

            if( !string.IsNullOrWhiteSpace(dto.SupervisorId) )
                proposals = proposals.Where( p => p.Student.SupervisorId ==  dto.SupervisorId ) ;

            if( !string.IsNullOrWhiteSpace(dto.SupervisorName) )
                proposals = proposals.Where( p => p.Student.Supervisor.AppUser.UserName.Contains( dto.SupervisorName ) );

            if( !string.IsNullOrWhiteSpace(dto.Title) )
                proposals = proposals.Where( p => p.Title.Contains( dto.Title ) );

            if( dto.Year.HasValue )
                proposals = proposals.Where( p => p.Year == dto.Year );

            if( dto.Session.HasValue )
                proposals = proposals.Where( p => p.Session == dto.Session );

            if( dto.Semester.HasValue )
                proposals = proposals.Where( p => p.Semester == dto.Semester );

            if( dto.ProposalStatus.HasValue )
                proposals = proposals.Where( p => p.ProposalStatus == dto.ProposalStatus );

            if( dto.Domain.HasValue )
                proposals = proposals.Where( p => p.Domain == dto.Domain );
            
            if( dto.MarkEqual.HasValue )
                proposals = proposals.Where( p => p.Mark == dto.MarkEqual);
            
            if( dto.MarkGreater.HasValue )
                proposals = proposals.Where( p => p.Mark > dto.MarkGreater);

            if( dto.MarkLower.HasValue )
                proposals = proposals.Where( p => p.Mark < dto.MarkLower);

            if( dto.CreatedAtGreater.HasValue  )
                proposals = proposals.Where( p => p.CreatedAt > dto.CreatedAtGreater );
            
            if( dto.CreatedAtLower.HasValue )
                proposals = proposals.Where( p => p.CreatedAt < dto.CreatedAtLower);

            if (!string.IsNullOrWhiteSpace(dto.CommitteeLecturerId))
            {

                var programList = await _context.Committees
                                                    .Where(p => p.LecturerId == dto.CommitteeLecturerId)
                                                    .Select(p => p.AcademicProgramId)
                                                    .ToListAsync();

                proposals = proposals.Where(p =>

                    programList.Contains(p.Student.AcademicProgramId)
                );
            }
        

            return ( await proposals.Select( p => new GetProposalResult{

                Id = p.Id,
                StudentId = p.StudentId,
                // StudentName = p.Student.AppUser.UserName,
                Year = p.Student.Year,
                Semester = p.Student.Semester,
                Session = p.Student.Session,
                // FirstEvaluatorId = p.Student.FirstEvaluatorId,
                // FirstEvaluatorName = p.Student.FirstEvaluator.AppUser.UserName,
                // SecondEvaluatorId = p.Student.SecondEvaluatorId,
                // SecondEvaluatorName = p.Student.SecondEvaluator.AppUser.UserName,
                // SupervisorId = p.Student.SupervisorId,
                // SupervisorName = p.Student.Supervisor.AppUser.UserName,
                Mark = p.Mark,
                Title = p.Title,
                Domain = p.Domain,
                ProposalStatus = p.ProposalStatus,
                AcademicProgramName = p.Student.AcademicProgram.Name,
                // Comments = p.Comments.Select(
                //     c => new GetCommentResult{
                        
                //         Id = c.Id,
                //         EvaluatorId = c.EvaluatorId,
                //         EvaluatorName = c.Evaluator.AppUser.UserName,
                //         CreatedAt = c.CreatedAt,
                //         Text = c.Text
                //     }
                // ),
                CreatedAt = p.CreatedAt
                }).AsNoTracking().ToListAsync(), null );
            
        }

        public async Task<GetProposalResult?> GetOneInfoAsync(string ProposalId)
        {
            return await _context.Proposals.Select( p => new GetProposalResult{

                Id = p.Id,
                StudentId = p.StudentId,
                StudentName = p.Student.AppUser.UserName,
                Year = p.Student.Year,
                Semester = p.Student.Semester,
                Session = p.Student.Session,
                FirstEvaluatorId = p.Student.FirstEvaluatorId,
                FirstEvaluatorName = p.Student.FirstEvaluator.AppUser.UserName,
                SecondEvaluatorId = p.Student.SecondEvaluatorId,
                SecondEvaluatorName = p.Student.SecondEvaluator.AppUser.UserName,
                SupervisorId = p.Student.SupervisorId,
                SupervisorName = p.Student.Supervisor.AppUser.UserName,
                Mark = p.Mark,
                Title = p.Title,
                Domain = p.Domain,
                CreatedAt = p.CreatedAt,
                ProposalStatus = p.ProposalStatus,
                AcademicProgramName = p.Student.AcademicProgram.Name,
                // Comments = p.Comments.Select(
                //     c => new GetCommentResult{
                        
                //         Id = c.Id,
                //         EvaluatorId = c.EvaluatorId,
                //         EvaluatorName = c.Evaluator.AppUser.UserName,
                //         CreatedAt = c.CreatedAt,
                //         Text = c.Text
                //     }
                // ),

            }).AsNoTracking().FirstOrDefaultAsync( p => p.Id == ProposalId );
        }

        public Task<string?> GetProposalLinkAsync(string ProposalId)
        {
            return _context.Proposals.Where(p => p.Id == ProposalId).AsNoTracking().Select( p => p.LinkProposal ).FirstOrDefaultAsync();
        }

        
        public Task<string?> GetFormLinkAsync(string ProposalId)
        {
            return _context.Proposals.Where(p => p.Id == ProposalId).AsNoTracking().Select( p => p.LinkForm ).FirstOrDefaultAsync();
        }

        public async Task<string?> UpdateMarkAsync(string ProposalId, UpdateMark dto)
        {

            var proposal = await _context.Proposals.FirstOrDefaultAsync( p => p.Id == ProposalId );

            if( proposal == null )
                return "No Such Proposal Exists";
            
            proposal.Mark = dto.Mark;

            var result = await _context.SaveChangesAsync();

            if(result >= 0)
                return null;
            
            return "Save Mark Failed, Please Retry Later";
        }

        public async Task<string?> UpdateStateAsync(string ProposalId, UpdateState dto)
        {
            var proposal = await _context.Proposals.FirstOrDefaultAsync( p => p.Id == ProposalId );

            if( proposal == null )
                return "No Such Proposal Exists";
            
            proposal.ProposalStatus = (ProposalStatus)dto.ProposalStatus;

            var result = await _context.SaveChangesAsync();

            if(result >= 0)
                return null;
            
            return "Save Status Failed, Please Retry Later";
        }
    }
}