using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using ProposalSystem.Data;
using ProposalSystem.Dtos.AcademicProgram;
using ProposalSystem.Dtos.Committee;
using ProposalSystem.Dtos.Lecturer;
using ProposalSystem.Dtos.Proposal;
using ProposalSystem.Interfaces;
using ProposalSystem.Models;

namespace ProposalSystem.Repository
{
    public class CommitteeRepository : ICommitteeRepository
    {
        private readonly ApplicationDbContext _context;

        public CommitteeRepository(ApplicationDbContext context){

            _context = context;
        }
        public async Task<(GetCommitteeResult?, string)> CreateAsync(CreateCommittee dto)
        {
            Committee committee = new Committee{

                AcademicProgramId = dto.AcademicProgramId,
                LecturerId = dto.LecturerId
            };

            await _context.Committees.AddAsync( committee );

            var result = await _context.SaveChangesAsync();

            if(result > 0){

                var LecturerInfo = await _context.Lecturers
                                    .Where( l => l.StaffId == dto.LecturerId )
                                    .Select( l => new{ 
                                            LecturerName = l.AppUser.UserName,
                                            Domain = l.Domain,
                                    } )
                                    .FirstOrDefaultAsync();
                
                var AcademicProgramInfo = await _context.AcademicPrograms
                                            .Where( a => a.Id == dto.AcademicProgramId )
                                            .FirstOrDefaultAsync();

                var response = new GetCommitteeResult(){

                    AcademicProgramId = dto.AcademicProgramId,
                    AcademicProgramName = AcademicProgramInfo.Name,
                    LecturerDomain = LecturerInfo.Domain,
                    LecturerName = LecturerInfo.LecturerName,
                    LecturerId = dto.LecturerId,
                    Id = committee.Id
                    
                };

                return (response, null);
            } 

            return (null, "Save Change Failed");

        }

        public async Task<string?> DeleteAsync(string Id)
        {
            var exist = await _context.Committees.AnyAsync( c => c.Id == Id);

            if(!exist)
                return "Committee Not Exists";

            var committee = new Committee(){ Id = Id };

            _context.Remove(committee);
            var result = await _context.SaveChangesAsync();

            if( result > 0 )
                return null;

            return "Save Change Failed";

        }

        public async Task<IEnumerable<GetCommitteeResult>?> GetByIdAsync(string Id)
        {
            return await _context.Committees
                                    .Where( c => c.AcademicProgramId == Id )
                                    .Select( c => new GetCommitteeResult{

                                        AcademicProgramId = c.AcademicProgramId,
                                        AcademicProgramName = c.AcademicProgram.Name,
                                        LecturerDomain = c.Lecturer.Domain,
                                        LecturerName = c.Lecturer.AppUser.UserName,
                                        LecturerId = c.LecturerId,
                                        Id = c.Id
                                    }).ToListAsync();
        }

        public async Task<ICollection<GetAcademicProgram>?> GetProgramsByLecturerId(string Id){

            return await _context.Committees
                                    .Where(c => c.LecturerId == Id)
                                    .Select(a => new GetAcademicProgram
                                    {

                                        Id = a.AcademicProgramId,
                                        Name = a.AcademicProgram!.Name,
                                        Description = a.AcademicProgram!.Description,
                                    }).ToListAsync();
        }

        
    }
}