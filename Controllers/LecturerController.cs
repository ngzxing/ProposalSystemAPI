using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProposalSystem.Dtos.Lecturer;
using ProposalSystem.Interfaces;
using ProposalSystem.Models;

namespace ProposalSystem.Controllers
{   
    [Route("api/lecturer")]
    [ApiController]
    public class LecturerController : ControllerBase
    {
        private readonly ILecturerRepository _lecRepo;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

    

        public LecturerController( ILecturerRepository lecRepo, IMapper mapper, UserManager<AppUser> userManager ){

            _lecRepo = lecRepo;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll( [FromQuery] QueryLecturer dto ){

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _lecRepo.GetAllAsync(dto);

            if (result == null){

                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById( [FromRoute] string Id ){

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _lecRepo.GetByIdAsync(Id);
            

            if (result == null){

                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] CreateLecturer dto){
            
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);


                Lecturer lecturer = _mapper.Map<Lecturer>( dto );

                var (newCreatedLecturer, result) = await _lecRepo.CreateAsync( _userManager, lecturer,  dto.Password);

                if( newCreatedLecturer != null ){

                    return Ok(  newCreatedLecturer  );

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
        public async Task<IActionResult> Update([FromRoute] string Id, [FromBody] UpdateLecturer dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var student = await _lecRepo.UpdateAsync(Id, _userManager, dto);

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

            var error_msg = await _lecRepo.DeleteAsync(Id, _userManager);

            if (error_msg != null)
            {
                return NotFound(error_msg);
            }

            return Ok();
        }

    }
}