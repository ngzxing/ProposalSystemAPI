using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProposalSystem.Interfaces;

namespace ProposalSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {   
        private readonly IAdminRepository _adminRepo;
        public AdminController( IAdminRepository adminRepo ){

            _adminRepo = adminRepo;
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById( [FromRoute] string Id ){

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _adminRepo.GetByIdAsync(Id);
            

            if (result == null){

                return NotFound();
            }

            return Ok(result);
        }
    }
}