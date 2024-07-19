using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProposalSystem.Dtos;
using ProposalSystem.Dtos.Student;
using ProposalSystem.Interfaces;
using ProposalSystem.Models;
using ProposalSystem.Repository;
using ProposalSystem.utils.mapper;

namespace ProposalSystem.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepo;
        private readonly IMapper _mapper;

        private readonly UserManager<AppUser> _userManager;

        public StudentController(IStudentRepository studentRepo, IMapper mapper, UserManager<AppUser> userManager)
        {
            _studentRepo = studentRepo;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById( [FromRoute] string Id ){

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _studentRepo.GetByIdAsync(Id);
            

            if (result == null){

                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll( [FromQuery] QueryStudent dto ){

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _studentRepo.GetAllAsync(dto);

            if (result == null){

                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Register( [FromBody] CreateStudent dto ){

            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);


                Student student = _mapper.Map<Student>( dto );

                var (success, result) = await _studentRepo.CreateAsync( _userManager, student,  dto.Password);

                if( success ){

                    return Ok( _mapper.Map<CreateStudentResult>( student ) );

                }else{

                    return StatusCode(500, result);
                }
                
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
        

        [HttpPut]
        [Route("{Id}")]
        public async Task<IActionResult> Update([FromRoute] string Id, [FromBody] UpdateStudent dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var student = await _studentRepo.UpdateAsync(Id, _userManager, dto);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        [HttpDelete]
        [Route("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] string Id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var error_msg = await _studentRepo.DeleteAsync(Id, _userManager);

            if (error_msg != null)
            {
                return NotFound(error_msg);
            }

            return Ok();
        }

        [HttpPut]
        [Route("{Id}/evaluator")]
        public async Task<IActionResult> AddEvaluator([FromRoute] string Id, [FromBody] UpdateEvaluator dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var (student, response) = await _studentRepo.AddEvaluatorAsync(Id, dto);

            if (student == null)
            {
                return NotFound(response);
            }

            return Ok(student);
        }


        [HttpDelete]
        [Route("{MatricId}/evaluator/{StaffId}")]
        public async Task<IActionResult> DeleteEvaluator([FromRoute] string MatricId, [FromRoute] string StaffId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var error_msg = await _studentRepo.DeleteEvaluatorAsync(MatricId, StaffId);

            if (error_msg != null)
            {
                return NotFound(error_msg);
            }

            return Ok();
        }

        [HttpDelete]
        [Route("{MatricId}/supervisor/{StaffId}")]
        public async Task<IActionResult> DeleteSupervisor([FromRoute] string MatricId, [FromRoute] string StaffId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var error_msg = await _studentRepo.DeleteSupervisorAsync(MatricId, StaffId);

            if (error_msg != null)
            {
                return NotFound(error_msg);
            }

            return Ok();
        }

        // [HttpPut]
        // [Route("{Id}/supervisor")]
        // public async Task<IActionResult> AddSupervisor([FromRoute] string Id, [FromBody] UpdateSupervisor dto)
        // {
        //     if (!ModelState.IsValid)
        //         return BadRequest(ModelState);

        //     var (student, response) = await _studentRepo.AddSupervisorAsync(Id, dto);

        //     if (student == null)
        //     {
        //         return NotFound(response);
        //     }

        //     return Ok(student);
        // }


    }
}