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


namespace AutismRehabilitationSystem.Controllers.ApiControllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JWTService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IStudentRepository _studentRepo;
        private readonly ILecturerRepository _lecturerRepo;
        private readonly IAdminRepository _adminRepo;
        private readonly ApplicationDbContext _context;
        private readonly IAcademicProgramRepository _programRepo;

        private readonly IMapper _mapper;
        // public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public AccountController(
            JWTService tokenService,
            SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            IStudentRepository studentRepo,
            ILecturerRepository lecturerRepo,
            IAdminRepository adminRepo,
            IMapper mapper,
            ApplicationDbContext context,
            IAcademicProgramRepository programRepo
            
        )
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
            _userManager = userManager;
            _studentRepo = studentRepo;
            _lecturerRepo = lecturerRepo;
            _adminRepo = adminRepo;
            _mapper = mapper;
            _context = context;
            _programRepo = programRepo;

            
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
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

            appUser.Logined = false;

            await _context.SaveChangesAsync();

            return Ok();
            
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            Admin? admin = await _context.Admin.Where( a => a.AdminId == login.Id! ).Include( a => a.AppUser ).FirstOrDefaultAsync();
            Lecturer? lecturer = null;
            Student? student = null;
            string userName;
            string Id;
            string role;
            AppUser appUser;

            if(admin == null){

                lecturer = await _context.Lecturers.Where( a => a.StaffId == login.Id! ).Include( a => a.AppUser ).FirstOrDefaultAsync();

                if(lecturer == null){

                    student = await _context.Students.Where( a => a.MatricId == login.Id! ).Include( a => a.AppUser ).FirstOrDefaultAsync();

                    if(student==null)
                        return NotFound("User Not Found");
                    
                    userName = student.AppUser!.UserName!;
                    Id = student.MatricId!;
                    role = "Student";
                    appUser = student.AppUser;

                }else{

                    userName = lecturer.AppUser!.UserName!;
                    Id = lecturer.StaffId!;
                    role = "Lecturer";
                    appUser = lecturer.AppUser;
                }

            }
            else{

                userName = admin.AppUser!.UserName!;
                Id = admin.AdminId!;
                role = "Admin";
                appUser = admin.AppUser;
            }


            if ((await _signInManager.PasswordSignInAsync(userName, login.Password!, false, lockoutOnFailure: false)).Succeeded)
            {

                
                var token = _tokenService.GenerateToken(Id, role, new Dictionary<string, string>());

                appUser.Logined = true;
                await _context.SaveChangesAsync();

                return Ok(new { token = token, id = Id, role = role });
            }

            return Unauthorized("Login Failed");
        }

        

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateStudent dto)
        {

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

        [HttpGet("programs")]
        public async Task<IActionResult> GetAllPrograms(){

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(await _programRepo.GetAllAsync());
        }

    }



    public class LoginModel
    {
        [Required]
        public string? Id { get; set; }

        [Required]
        public string? Password { get; set; }
    }

}


