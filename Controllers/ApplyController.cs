using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ProposalSystem.Dtos.Lecturer;
using ProposalSystem.Interfaces;
using ProposalSystem.Models;
using ProposalSystem.Dtos.ApplySupervisor;
using ProposalSystem.Dtos.Student;

namespace ProposalSystem.Controllers
{   
    [Route("api/Apply")]
    [ApiController]
    public class ApplyController : ControllerBase
    {
        private readonly IApplyRepository _applyRepo;
        private readonly IStudentRepository _studentRepo;

        public ApplyController( IApplyRepository applyRepo, IStudentRepository studentRepo ){

            _applyRepo = applyRepo;
            _studentRepo = studentRepo;
        }

        [HttpPost]
        public async Task<IActionResult> Create( [FromBody]CreateApply dto ){

            var (result, error_msg) =  await _applyRepo.CreateAsync( dto.MatricId!, dto.StaffId! );

            if(result == null)
                return NotFound(error_msg);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll( [FromQuery] QueryApply dto ){

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _applyRepo.GetAllAsync(dto);

            if (result == null){

                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById( [FromRoute] string Id ){

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _applyRepo.GetByIdAsync(Id);
            

            if (result == null){

                return NotFound();
            }

            return Ok(result);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> ChangeState( [FromRoute] string Id, [FromBody] ChangeState dto  ){

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var err_msg = await _applyRepo.ChangeStateAsync(Id, dto);
            

            if (err_msg == null)
                return Ok();
            

            return NotFound(err_msg);
        }

        [HttpPut("confirm/{Id}")]
        public async Task<IActionResult> ConfirmApply( [FromRoute] string Id ){

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var apply = await _applyRepo.GetByIdAsync(Id);

            if(apply == null)
                return NotFound("No Such Application Available");
        
            UpdateSupervisor dto = new UpdateSupervisor(){ SupervisorId = apply.SupervisorId };
            var (student, err_msg) = await _studentRepo.AddSupervisorAsync(apply.MatricId!, dto);

            if (student == null)
                return NotFound(err_msg);
            
            err_msg = await _applyRepo.DeleteAsync( Id );

            if (err_msg == null)
                return Ok(student);

            return NotFound(err_msg);
        }

    }
}