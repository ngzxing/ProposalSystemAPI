using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProposalSystem.Data;
using ProposalSystem.Dtos.Lecturer;
using ProposalSystem.Interfaces;
using ProposalSystem.Models;

namespace ProposalSystem.Repository
{
    public class LecturerRepository : ILecturerRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public LecturerRepository(ApplicationDbContext context, IMapper mapper){

            _context = context;
            _mapper = mapper;
        }
        public async Task< (CreateLecturerResult?, string ) > CreateAsync(UserManager<AppUser> userManager, Lecturer dto, string password)
        {   

            var exist = await _context.Lecturers.AnyAsync( s => string.Equals( s.StaffId, dto.StaffId ) );

            if(exist)
                return (null, "Your Staff Code Has Been Registered");

            var programExist = await _context.AcademicPrograms.AnyAsync(a => a.Id == dto.AcademicProgramId);
            if (!programExist)
                return (null, "The Academic Program Does Not Exist");

            var newUserResponse = await userManager.CreateAsync( dto.AppUser, password );

            if( !newUserResponse.Succeeded ){

                return ( null, "Create User Failed" );
                
            }

            var addRoleResponse = await userManager.AddToRoleAsync( dto.AppUser, "Lecturer" );

            Console.WriteLine("haha");
            if( !addRoleResponse.Succeeded ){

                return ( null, "Assign Role Failed" );

            }

            await _context.Lecturers.AddAsync( dto );
            await _context.SaveChangesAsync();

            return ( _mapper.Map<CreateLecturerResult>(dto)  , "Create Success" );
        }

        public async Task<string?> DeleteAsync(string StaffId, UserManager<AppUser> userManager)
        {
            var lecturer = await _context.Lecturers
                                            .Include( l => l.SupervisedStudents )
                                            .Include( l => l.FirstEvaluatedStudents)
                                            .Include( l => l.SecondEvaluatedStudents )
                                            .Include( l => l.Comments)
                                            .Include( l => l.Committee)
                                            .Include( l => l.Applications )
                                            .FirstOrDefaultAsync( s => s.StaffId == StaffId );
            

            if( lecturer == null )
                return "Lecturer Not Found";

            var appUser = await userManager.FindByIdAsync( lecturer.Id );
            var result = await userManager.DeleteAsync( appUser );
            
            if( result.Succeeded )
                return null;
            else
                return "Delete Lecturer Failed For Unknown Reason";
        }

        public Task<bool> ExistAsync(string Id)
        {
            return _context.Lecturers.AnyAsync( l => l.StaffId == Id );
        }

        public async Task<List<GetLecturerResult>> GetAllAsync(QueryLecturer? query)
        {
            var lecturer = _context.Lecturers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.UserName))
            {
                lecturer = lecturer.Where(l => l.AppUser.UserName.Contains(query.UserName) );
            }
            if (!string.IsNullOrWhiteSpace(query.PhoneNumber))
            {
                lecturer = lecturer.Where(s => s.AppUser.PhoneNumber == query.PhoneNumber);
            }
            if (!string.IsNullOrWhiteSpace(query.Email))
            {
                lecturer = lecturer.Where(s => s.AppUser.Email == query.Email);
            }
            if (query.Domain.HasValue)
            {
                lecturer = lecturer.Where(l => l.Domain == query.Domain);
            }
            if (!string.IsNullOrWhiteSpace(query.CommitteeLecturerId))
            {

                var programList = await _context.Committees
                                                    .Where(p => p.LecturerId == query.CommitteeLecturerId)
                                                    .Select(p => p.AcademicProgramId)
                                                    .ToListAsync();

                lecturer = lecturer.Where(l =>

                    programList.Contains(l.AcademicProgramId)
                );
            }


            return await lecturer.Select( l => new GetLecturerResult{

                UserName = l.AppUser.UserName,
                StaffId = l.StaffId,
                PhoneNumber = l.AppUser.PhoneNumber,
                Email = l.AppUser.Email,
                Domain = l.Domain,
                AcademicProgramId = l.AcademicProgramId,
                AcademicProgramName = l.AcademicProgram.Name
                }
                ).AsNoTracking().ToListAsync();
        }

        public async Task<GetLecturerResult?> GetByIdAsync(string StaffId)
        {
            return await _context.Lecturers.Where(l => l.StaffId == StaffId).Select( l => new GetLecturerResult{

                UserName = l.AppUser.UserName,
                StaffId = l.StaffId,
                PhoneNumber = l.AppUser.PhoneNumber,
                Email = l.AppUser.Email,
                Domain = l.Domain,
                AcademicProgramName = l.AcademicProgram.Name
                }
                ).AsNoTracking().FirstOrDefaultAsync( );
        }

        public async Task<UpdateLecturerResult?> UpdateAsync(string StaffId, UserManager<AppUser> userManager, UpdateLecturer dto)
        {
            var lecturer = await _context.Lecturers
                                            .Where( l => l.StaffId == StaffId )
                                            .Include( l => l.AppUser )
                                            .FirstOrDefaultAsync();

            
            if (lecturer == null){

                return null;
            }


            _mapper.Map<UpdateLecturer, Lecturer>(dto, lecturer);
            await _context.SaveChangesAsync();

            var updateStudent = new UpdateLecturerResult{

                StaffId = StaffId,
                Email = dto.Email,
                UserName = dto.UserName,
                PhoneNumber = dto.PhoneNumber,
                Domain = dto.Domain
            };

            return updateStudent;
        }
    }
}