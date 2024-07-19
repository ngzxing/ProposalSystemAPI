using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using ProposalSystem.Data;
using ProposalSystem.Data.Enum;
using ProposalSystem.Dtos.ApplySupervisor;
using ProposalSystem.Interfaces;
using ProposalSystem.Models;

namespace ProposalSystem.Repository
{
    public class ApplyRepository : IApplyRepository
    {
        private readonly ApplicationDbContext _context;

        public ApplyRepository( ApplicationDbContext context ){

            _context = context;
        }

        public async Task<string?> ChangeStateAsync(string Id, ChangeState dto)
        {
            var apply = await _context.ApplySupervisors.FirstOrDefaultAsync( s => s.Id == Id );

            if( apply == null )
                return "Student Not Found";

            apply.ApplyState = dto.status;

            var result = await _context.SaveChangesAsync();

            if(result > 0)
                return null;
            
            return "Update Application Status Failed For Unknown Reason";
        }

        public async Task<(ApplySupervisor?, string? )> CreateAsync(string MatricId, string StaffId)
        {
            Student? student = await _context.Students.FirstOrDefaultAsync( s => s.MatricId == MatricId );

            if( student == null )
                return (null, "Student Not Found");
            
            bool staffExists = await _context.Lecturers.AnyAsync( l => l.StaffId == StaffId );

            if( !staffExists )
                return (null, "Lecturer Not Found");
            
            if(student.FirstEvaluatorId == StaffId || student.SecondEvaluatorId == StaffId){

                return (null, "Supervisor cannot be the evaluator");
            }



            ApplySupervisor apply = new(){
                
                SupervisorId = StaffId,
                MatricId = MatricId,
                ApplyState = ApplySupervisorStatus.Pending
            };
            
            await _context.ApplySupervisors.AddAsync(apply);
            await _context.SaveChangesAsync();

            return (apply, null); 


        }

        public async Task<string?> DeleteAsync(string ApplyId)
        {
            var exists = await _context.ApplySupervisors.AnyAsync( a => a.Id == ApplyId );

            if(!exists)
                return "Application Record Not Found";

            _context.ApplySupervisors.Remove( new ApplySupervisor(){ Id = ApplyId} );

            var result = await _context.SaveChangesAsync();

            if(result > 0)
                return null;

            return "Delete Application Result Failed For Unknown Reason";



        }

        public async Task< List<GetApplyResult>?> GetAllAsync(QueryApply query)
        {
            var apply = _context.ApplySupervisors.AsQueryable();

            if( query.Domain.HasValue )
                apply = apply.Where( a => a.Supervisor.Domain == query.Domain );

            if( query.ApplyState.HasValue )
                apply = apply.Where( a => a.ApplyState == query.ApplyState );
            
            if( !string.IsNullOrWhiteSpace( query.MatricId) )
                apply = apply.Where( a => a.MatricId == query.MatricId );

            if( !string.IsNullOrWhiteSpace( query.SupervisorId) )
                apply = apply.Where( a => a.SupervisorId == query.SupervisorId );

            if( !string.IsNullOrWhiteSpace( query.StudentName) )
                apply = apply.Where( a => a.Student.AppUser.UserName.Contains( query.StudentName )  );

            if( !string.IsNullOrWhiteSpace( query.SupervisorName) )
                apply = apply.Where( a => a.Supervisor.AppUser.UserName.Contains( query.SupervisorName )  );

            if( query.Year.HasValue )
                apply = apply.Where( a => a.Student.Year == query.Year );
            
            if( query.Semester.HasValue )
                apply = apply.Where( a => a.Student.Semester == query.Semester );

            if( query.Session.HasValue )
                apply = apply.Where( a => a.Student.Session == query.Session );

            if (!string.IsNullOrWhiteSpace(query.CommitteeLecturerId))
            {

                var programList = await _context.Committees
                                                    .Where(p => p.LecturerId == query.CommitteeLecturerId)
                                                    .Select(p => p.AcademicProgramId)
                                                    .ToListAsync();

                apply = apply.Where(a =>

                    programList.Contains(a.Student.AcademicProgramId)
                );
            }

            return await apply.Select( a => new GetApplyResult(){

                Id = a.Id,
                MatricId = a.MatricId,
                SupervisorId = a.SupervisorId,
                StudentName = a.Student.AppUser.UserName,
                Domain = a.Supervisor.Domain,
                Year = a.Student.Year,
                Semester = a.Student.Semester,
                Session = a.Student.Session,
                SupervisorName = a.Supervisor.AppUser.UserName,
                status = a.ApplyState
            }
            ).AsNoTracking().ToListAsync();

        }

        public async Task<GetApplyResult?> GetByIdAsync( string Id ){

            return await _context.ApplySupervisors.Select( a => new GetApplyResult(){

                Id = a.Id,
                MatricId = a.MatricId,
                SupervisorId = a.SupervisorId,
                StudentName = a.Student.AppUser.UserName,
                Domain = a.Supervisor.Domain,
                Year = a.Student.Year,
                Semester = a.Student.Semester,
                Session = a.Student.Session,
                SupervisorName = a.Supervisor.AppUser.UserName,
                status = a.ApplyState
            }
            ).AsNoTracking().FirstOrDefaultAsync( a => a.Id == Id );

        }

        
    }
}