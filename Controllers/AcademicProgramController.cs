using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProposalSystem.Dtos.AcademicProgram;
using ProposalSystem.Interfaces;

namespace ProposalSystem.Controllers
{   
    [Route("api/program")]
    [ApiController]
    public class AcademicProgramController : ControllerBase
    {
        private readonly IAcademicProgramRepository _programRepo;

        public AcademicProgramController(IAcademicProgramRepository programRepo){

            _programRepo = programRepo;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAcademicProgram dto){

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var program = await _programRepo.CreateAsync(dto);

            if(program == null)
                return StatusCode(500, "Create Academic Program Failed");

            return Ok(program);

        }

        [HttpGet]
        public async Task<IActionResult> GetAll(){

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(await _programRepo.GetAllAsync());
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute]string Id){

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var program = await _programRepo.GetByIdAsync( Id );

            if(program == null)
                return NotFound("Academic Program Not Exists");

            return Ok(program);
        }

        [HttpGet("NotCommittee/{Id}")]
        public async Task<IActionResult> NotCommittee( [FromRoute] string Id ){

            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var result = await _programRepo.GetLecturerNotCommittee(Id);

            if(result == null)
                return NotFound("No Committee Record Found");
            
            return Ok(result);
            
        }
    }
}