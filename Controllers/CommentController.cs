using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ProposalSystem.Dtos.Comment;
using ProposalSystem.Interfaces;

namespace ProposalSystem.Controllers
{   
    [Route( "api/comment" )]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IProposalRepository _proposalRepo;
        private readonly ILecturerRepository _lecturerRepo;
        private readonly IStudentRepository _studentRepo;

        public CommentController( ICommentRepository commentRepo, IProposalRepository proposalRepo, ILecturerRepository lecturerRepo, IStudentRepository studentRepo ){

            _commentRepo = commentRepo;
            _proposalRepo = proposalRepo;
            _lecturerRepo = lecturerRepo;
            _studentRepo = studentRepo;
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(string Id){

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await _commentRepo.GetByIdAsync(Id);

            if( comment == null )
                return NotFound("Comment Not Found");

            return Ok(comment);
        } 

        [HttpGet("proposal/{ProposalId}")]
        public async Task<IActionResult>GetByProposalId(string ProposalId){

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var proposal = await _proposalRepo.GetProposalLinkAsync( ProposalId );

            if( proposal == null )
                return NotFound("Proposal Not Found");

            var comments = await _commentRepo.GetByProposalIdAsync(ProposalId);

            return Ok(comments);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateComment dto){

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var proposal = await _proposalRepo.GetOneInfoAsync( dto.ProposalId );

            if( proposal == null )
                return NotFound("Proposal Not Found");

            if( (proposal.FirstEvaluatorId != dto.EvaluatorId) && (proposal.SecondEvaluatorId != dto.EvaluatorId) && (proposal.SupervisorId != dto.EvaluatorId) )
                return BadRequest("The Lecturer Is Neither Evaluator of This Proposal Nor Supervisor of this Student");
            
            var (comment, err_msg) = await _commentRepo.CreateAsync( dto );

            if (err_msg != null )
                return StatusCode(500, err_msg);

            return Ok(comment);

        }

        [HttpDelete("{CommentId}")]
        public async Task<IActionResult> Delete( [FromRoute]string CommentId ){

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = _commentRepo.GetByIdAsync( CommentId );

            if ( comment == null )
                return NotFound("Comment Not Found");
            
            string? err_msg = await _commentRepo.DeleteCommentAsync(CommentId);

            if( err_msg != null )
                return StatusCode(500, err_msg);

            return Ok();
        }
    }
}