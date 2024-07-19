using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProposalSystem.Data;
using ProposalSystem.Dtos.Proposal;
using ProposalSystem.Interfaces;

namespace ProposalSystem.Controllers
{   
    [Route("api/proposal")]
    [ApiController]
    public class ProposalController : ControllerBase
    {
        private readonly ICloudService _pdfService;
        private readonly IProposalRepository _proposalRepo;
        private readonly IStudentRepository _studentRepo;
        private readonly ApplicationDbContext _context;

        public ProposalController( IProposalRepository proposalRepo, ICloudService pdfService, IStudentRepository studentRepo, ApplicationDbContext context ){

            _pdfService = pdfService;
            _proposalRepo = proposalRepo;
            _studentRepo = studentRepo;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateProposal dto){

            var student = await _studentRepo.GetByIdAsync( dto.StudentId );

            if ( student == null)
                return NotFound( "Student Not Found" );
            
            var (proposalFileId, err_msg_proposal) = await _pdfService.AddPDFAsync(dto.ProposalFile);

            if( err_msg_proposal != null )
                return StatusCode(500, err_msg_proposal);

            var (formFileId, err_msg_form) = await _pdfService.AddPDFAsync(dto.FormFile);

            if( err_msg_form != null )
                return StatusCode(500, err_msg_form);

            var (proposal, err_msg) = await _proposalRepo.CreateAsync( dto, proposalFileId, formFileId, student );
            
            if( proposal == null )
                return StatusCode(500, err_msg);
            
            return Ok(proposal);

        }

        [HttpDelete("{ProposalId}")]
        public async Task<IActionResult> Delete(string ProposalId){

            var proposal = await _proposalRepo.GetProposalLinkAsync(ProposalId);
            if(proposal == null)
                return NotFound("Proposal Not Found");

            var deletePdf_err_msg = await _pdfService.DeletePDFAsync(proposal);

            Console.WriteLine( deletePdf_err_msg );

            if(deletePdf_err_msg != null)
                return StatusCode(500, deletePdf_err_msg);

            var deleteProposal_err_msg = await _proposalRepo.DeleteAsync(ProposalId);

            if(deleteProposal_err_msg != null)
                return StatusCode(500, deleteProposal_err_msg);

            return Ok();
        }

        [HttpGet("file/{ProposalId}")]
        public async Task<IActionResult> GetProposalById(string ProposalId){

            var proposal = await _proposalRepo.GetProposalLinkAsync(ProposalId);

            if(proposal == null)
                return NotFound("Proposal Not Found");

            var (pdfContent, err_msg) = await _pdfService.GetPDFByIdAsync(proposal);

            if(pdfContent == null)
                return StatusCode(500, err_msg);
            
            return File(pdfContent.ToArray(), "application/pdf","proposal.pdf");
        }

        [HttpGet("form/{ProposalId}")]
        public async Task<IActionResult> GetFormById(string ProposalId){

            var form = await _proposalRepo.GetFormLinkAsync(ProposalId);

            if(form == null)
                return NotFound("Proposal Not Found");

            var (pdfContent, err_msg) = await _pdfService.GetPDFByIdAsync(form);

            if(pdfContent == null)
                return StatusCode(500, err_msg);
            
            return File(pdfContent.ToArray(), "application/pdf","signedform.pdf");
        }

        [HttpGet("form")]
        public async Task<IActionResult> GetFormAsync(){

            

            var (pdfContent, err_msg) = await _pdfService.GetPDFByIdAsync("1o8s4xOvuPbZVN-LhpV-IhwE54-Z85VAy");

            if(pdfContent == null)
                return StatusCode(500, err_msg);
            
            return File(pdfContent.ToArray(), "application/pdf","form.pdf");
        }

        [HttpGet("info/{Id}")]
        public async Task<IActionResult> GetById( [FromRoute] string Id ){

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _proposalRepo.GetOneInfoAsync(Id);
            

            if (result == null){

                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("info")]
        public async Task<IActionResult> GetAll( [FromQuery] QueryProposal dto ){

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ( result, err_msg) = await _proposalRepo.GetAllInfoAsync(dto);

            if (err_msg != null)
                return BadRequest(err_msg);
            
            return Ok(result);
        }

        [HttpPut("status/{Id}")]
        public async Task<IActionResult> UpdateStatus( [FromRoute] string Id, [FromBody] UpdateState dto ){


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var proposal = await _proposalRepo.GetOneInfoAsync(Id);

            if( proposal == null)
                return NotFound("No Such Proposal Exist");

            if( (proposal.FirstEvaluatorId != dto.EvaluatorId) && (proposal.SecondEvaluatorId != dto.EvaluatorId) )
                return BadRequest("Your Are Not The Evaluator Of This Student");

            var err_msg = await _proposalRepo.UpdateStateAsync( Id, dto);

            if (err_msg != null)
                return BadRequest(err_msg);
            
            return Ok();
        }

        [HttpPut("mark/{Id}")]
        public async Task<IActionResult> UpdateMark( [FromRoute] string Id, [FromBody] UpdateMark dto ){

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var proposal = await _proposalRepo.GetOneInfoAsync(Id);
            if( proposal == null)
                return NotFound("No Such Proposal Exist");

            if( (proposal.FirstEvaluatorId != dto.EvaluatorId) && (proposal.SecondEvaluatorId != dto.EvaluatorId) )
                return BadRequest("Your Are Not The Evaluator Of This Student");

            var err_msg = await _proposalRepo.UpdateMarkAsync( Id, dto);

            if (err_msg != null)
                return BadRequest(err_msg);
            
            return Ok();
        }


    }
}