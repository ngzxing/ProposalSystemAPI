using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProposalSystem.Dtos.Committee;
using ProposalSystem.Interfaces;

namespace ProposalSystem.Controllers
{
    [Route("api/committee")]
    [ApiController]
    public class CommitteeController : ControllerBase
    {
        private readonly ICommitteeRepository _committeeRepo;
        private readonly ILecturerRepository _lecturerRepo;
        private readonly IAcademicProgramRepository _academicProgramRepo;

        public CommitteeController( ICommitteeRepository committeeRepo, ILecturerRepository lecturerRepo, IAcademicProgramRepository academicProgramRepo){

            _committeeRepo = committeeRepo;
            _lecturerRepo = lecturerRepo;
            _academicProgramRepo = academicProgramRepo;
        }

        [HttpGet("{ProgramId}")]
        public async Task<IActionResult> GetAllByProgramId( [FromRoute] string ProgramId ){

            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var result = await _committeeRepo.GetByIdAsync(ProgramId);

            if(result == null)
                return NotFound("No Committee Record Found");
            
            return Ok(result);
            
        }

        [HttpGet("programs/{Id}")]
        public async Task<IActionResult> GetAllPrograms([FromRoute] string Id){

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(await _committeeRepo.GetProgramsByLecturerId(Id));
        }



        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCommittee dto ){

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if ( !await _lecturerRepo.ExistAsync(dto.LecturerId) )
                return NotFound("Lecturer Not Found");
            
            if( await _academicProgramRepo.GetByIdAsync(dto.AcademicProgramId) == null )
                return NotFound("Academic Program Not Found");

            var (result, err_msg) = await _committeeRepo.CreateAsync(dto);

            if( err_msg != null )
                return StatusCode(500, err_msg);

            return Ok(result);

        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] string Id){

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var err_msg = await _committeeRepo.DeleteAsync(Id);

            if(err_msg!= null)
                return NotFound(err_msg);
            
            return Ok("Delete Success!");
        }
    }
}