using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing.Printing;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using AutismRehabilitationSystem.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using ProposalSystem.Data;
using ProposalSystem.Dtos.Admin;
using ProposalSystem.Dtos.Lecturer;
using ProposalSystem.Dtos.Student;
using ProposalSystem.Interfaces;
using ProposalSystem.Models;
using ProposalSystem.Repository;

namespace ProposalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        
        public ConnectionController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("check")]
        public async Task<IActionResult> Check(){

            var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            if(userId == null){
                userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            }

            string? role = User.FindFirstValue(ClaimTypes.Role);


            if( userId == null || role == null){

                return Unauthorized("Invalid Operation");
            }

            AppUser? appUser;

            if( role == "Admin" ){

                appUser = await _context.Admin.Where( a => a.AdminId == userId ).Select( a => a.AppUser ).FirstOrDefaultAsync();
            }
            else if( role == "Student" ){

                appUser = await _context.Students.Where( a => a.MatricId == userId ).Select( a => a.AppUser ).FirstOrDefaultAsync();
            }
            else if( role == "Lecturer" ){

                appUser = await _context.Lecturers.Where( a => a.StaffId == userId ).Select( a => a.AppUser ).FirstOrDefaultAsync();

            }else{

                return NotFound("Role Not Found");
            }

            if( appUser == null ){

                return NotFound("User Not Found");
            }

            
            if(!appUser.Logined){

                return Unauthorized("You Had Logout");
            }
            

            return Ok();
        }

    }
}