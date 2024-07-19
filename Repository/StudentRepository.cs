using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProposalSystem.Data;
using ProposalSystem.Dtos;
using ProposalSystem.Dtos.Student;
using ProposalSystem.Interfaces;
using ProposalSystem.Models;
using ProposalSystem.utils.extensions;

namespace ProposalSystem.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public StudentRepository(ApplicationDbContext context, IMapper mapper)
        {

            _context = context;
            _mapper = mapper;
        }

        public async Task<(GetStudentResult?, string)> AddEvaluatorAsync(string studentId, UpdateEvaluator dto)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.MatricId == studentId);
            if (student == null)
                return (null, "Student Not Found");

            if (student.SupervisorId == dto.EvaluatorId)
                return (null, "Supervisor Cannot Be The Evaluator");

            var lecturer = await _context.Lecturers
                                            .Select(l => new
                                            {

                                                StaffId = l.StaffId,
                                                UserName = l.AppUser.UserName,
                                                Domain = l.Domain
                                            })
                                            .FirstOrDefaultAsync(l => l.StaffId == dto.EvaluatorId);

            if (lecturer == null)
                return (null, "Lecturer Not Found");

            if (lecturer.Domain != dto.Domain)
                return (null, "The Evaluator Should Be The Same Domain");


            if (dto.who == 0)
            {

                if (student.SecondEvaluatorId == lecturer.StaffId)
                    return (null, "Duplicated Evaluator");

                student.FirstEvaluatorId = lecturer.StaffId;
            }
            else
            {

                if (student.FirstEvaluatorId == lecturer.StaffId)
                    return (null, "Duplicated Evaluator");
                student.SecondEvaluatorId = lecturer.StaffId;
            }

            await _context.SaveChangesAsync();

            var result = _mapper.Map<GetStudentResult>(student);

            if (dto.who == 0)
                result.FirstEvaluatorName = lecturer.UserName;
            else
                result.SecondEvaluatorName = lecturer.UserName;

            return (result, "Success");

        }

        public async Task<(GetStudentResult?, string)> AddSupervisorAsync(string studentId, UpdateSupervisor dto)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.MatricId == studentId);
            if (student == null)
                return (null, "Student Not Found");

            if ((student.FirstEvaluatorId == dto.SupervisorId) || (student.SecondEvaluatorId == dto.SupervisorId))
                return (null, "Evaluator Cannot Be The Supervisor");

            var lecturer = await _context.Lecturers
                                            .Select(l => new
                                            {

                                                StaffId = l.StaffId,
                                                UserName = l.AppUser.UserName
                                            }
                                            )
                                            .FirstOrDefaultAsync(l => l.StaffId == dto.SupervisorId);
            if (lecturer == null)
                return (null, "Lecturer Not Found");


            student.SupervisorId = lecturer.StaffId;

            await _context.SaveChangesAsync();

            var result = _mapper.Map<GetStudentResult>(student);
            result.SupervisorName = lecturer.UserName;

            return (result, "Success");
        }

        public async Task<(bool, Object)> CreateAsync(UserManager<AppUser> userManager, Student dto, string password)
        {
            var exist = await _context.Students.AnyAsync(s => s.MatricId.ToLower() == dto.MatricId.ToLower());
            if (exist)
                return (false, "Your Matric Code Has Been Registered");

            var programExist = await _context.AcademicPrograms.AnyAsync(a => a.Id == dto.AcademicProgramId);
            if (!programExist)
                return (false, "The Academic Program Does Not Exist");

            var newUserResponse = await userManager.CreateAsync(dto.AppUser!, password);

            if (!newUserResponse.Succeeded)
            {

                return (false, "Create User Failed");

            }

            var addRoleResponse = await userManager.AddToRoleAsync(dto.AppUser!, "Student");

            if (!addRoleResponse.Succeeded)
            {

                return (false, "Assign Role Failed");

            }

            await _context.Students.AddAsync(dto);
            await _context.SaveChangesAsync();

            return (true, "Create Success");
        }

        public async Task<string?> DeleteAsync(string MatricId, UserManager<AppUser> userManager)
        {
            var student = await _context.Students
                                            .Include<Student, ICollection<Proposal>>(s => s.Proposals)
                                                .ThenInclude(p => p.Comments)
                                            .Include(s => s.Applications)
                                            .FirstOrDefaultAsync(s => s.MatricId == MatricId);

            if (student == null)
                return "Student Not Found";


            var appUser = await userManager.FindByIdAsync(student.Id);
            var result = await userManager.DeleteAsync(appUser);

            if (result.Succeeded)
                return null;
            else
                return "Delete Student Failed For Unknown Reason";


        }

        public async Task<string?> DeleteEvaluatorAsync(string MatricId, string staffId)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.MatricId == MatricId);
            if (student == null)
                return "Student Not Found";


            if (student.FirstEvaluatorId == staffId)
                student.FirstEvaluatorId = null;

            else if (student.SecondEvaluatorId == staffId)
                student.SecondEvaluatorId = null;

            else
                return "No Such Evaluator For That Student";

            await _context.SaveChangesAsync();

            return null;
        }

        public async Task<string?> DeleteSupervisorAsync(string MatricId, string StaffId)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.MatricId == MatricId);
            if (student == null)
                return "Student Not Found";


            if (student.SupervisorId == StaffId)
                student.SupervisorId = null;
            else
                return "No Such Evaluator For That Student";

            await _context.SaveChangesAsync();

            return null;
        }

        public async Task<List<GetStudentResult>> GetAllAsync(QueryStudent? query)
        {
            var students = _context.Students.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.UserName))
            {
                students = students.Where(s => s.AppUser.UserName.Contains(query.UserName));
            }
            if (query.Year.HasValue)
            {
                students = students.Where(s => s.Year == query.Year);
            }
            if (!string.IsNullOrWhiteSpace(query.PhoneNumber))
            {
                students = students.Where(s => s.AppUser.PhoneNumber == query.PhoneNumber);
            }
            if (!string.IsNullOrWhiteSpace(query.Email))
            {
                students = students.Where(s => s.AppUser.Email == query.Email);
            }
            if (query.Session.HasValue)
            {
                students = students.Where(s => s.Session == query.Session);
            }

            if (query.Semester.HasValue)
            {
                students = students.Where(s => s.Semester == query.Semester);
            }

            if (!string.IsNullOrWhiteSpace(query.SupervisorId))
            {
                students = students.Where(s => s.SupervisorId == query.SupervisorId);
            }

            if (!string.IsNullOrWhiteSpace(query.SupervisorName))
            {
                students = students.Where(s =>
                    s.Supervisor.AppUser.UserName.Contains(query.SupervisorName)
                );
            }

            if (!string.IsNullOrWhiteSpace(query.EvaluatorId))
            {
                students = students.Where(s =>
                    (s.FirstEvaluatorId == query.EvaluatorId) || (s.SecondEvaluatorId == query.EvaluatorId)
                );
            }

            if (!string.IsNullOrWhiteSpace(query.EvaluatorName))
            {
                students = students.Where(s =>
                    s.FirstEvaluator.AppUser.UserName.Contains(query.EvaluatorName) || s.SecondEvaluator.AppUser.UserName.Contains(query.EvaluatorName)
                );
            }

            if (!string.IsNullOrWhiteSpace(query.CommitteeLecturerId))
            {

                var programList = await _context.Committees
                                                    .Where(p => p.LecturerId == query.CommitteeLecturerId)
                                                    .Select(p => p.AcademicProgramId)
                                                    .ToListAsync();

            

                students = students.Where(s =>

                    programList.Contains(s.AcademicProgramId)
                );
            }



            return await students.Select(s => new GetStudentResult
            {

                UserName = s.AppUser.UserName,
                MatricId = s.MatricId,
                Email = s.AppUser.Email,
                Session = s.Session,
                Year = s.Year,
                Semester = s.Semester,
                SupervisorId = s.SupervisorId,
                SupervisorName = s.Supervisor.AppUser.UserName,
                FirstEvaluatorId = s.FirstEvaluatorId,
                FirstEvaluatorName = s.FirstEvaluator.AppUser.UserName,
                SecondEvaluatorId = s.SecondEvaluatorId,
                SecondEvaluatorName = s.SecondEvaluator.AppUser.UserName,
                PhoneNumber = s.AppUser.PhoneNumber,
                AcademicProgramId = s.AcademicProgramId,
                AcademicProgramName = s.AcademicProgram.Name

            }
                ).AsNoTracking().ToListAsync();
        }

        public async Task<GetStudentResult?> GetByIdAsync(string id)
        {
            return await _context.Students.Where(s => s.MatricId == id).Select(s => new GetStudentResult
            {

                UserName = s.AppUser.UserName,
                MatricId = s.MatricId,
                Email = s.AppUser.Email,
                Session = s.Session,
                Year = s.Year,
                Semester = s.Semester,
                SupervisorId = s.SupervisorId,
                SupervisorName = s.Supervisor.AppUser.UserName,
                FirstEvaluatorId = s.FirstEvaluatorId,
                FirstEvaluatorName = s.FirstEvaluator.AppUser.UserName,
                SecondEvaluatorId = s.SecondEvaluatorId,
                SecondEvaluatorName = s.SecondEvaluator.AppUser.UserName,
                PhoneNumber = s.AppUser.PhoneNumber,
                AcademicProgramId = s.AcademicProgramId,
                AcademicProgramName = s.AcademicProgram.Name

            }
                ).AsNoTracking().FirstOrDefaultAsync();

        }

        public async Task<Student?> GetSimpleByIdAsync(string Id)
        {
            return await _context.Students.AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<UpdateStudentResult?> UpdateAsync(string MatricId, UserManager<AppUser> userManager, UpdateStudent dto)
        {

            var student = await _context.Students.FirstOrDefaultAsync(s => s.MatricId == MatricId);

            if (student == null)
            {

                return null;
            }

            var appUser = await userManager.FindByIdAsync(student.Id);

            _mapper.Map<UpdateStudent, AppUser>(dto, appUser);

            await _context.SaveChangesAsync();

            var updateStudent = new UpdateStudentResult
            {

                MatricId = MatricId,
                Email = dto.Email,
                UserName = dto.UserName,
                PhoneNumber = dto.PhoneNumber
            };

            return updateStudent;


        }
    }
}