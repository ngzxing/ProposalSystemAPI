using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using ProposalSystem.Data;
using ProposalSystem.Dtos.AcademicProgram;
using ProposalSystem.Dtos.Lecturer;
using ProposalSystem.Interfaces;
using ProposalSystem.Models;

namespace ProposalSystem.Repository
{
    public class AcademicProgramRepository : IAcademicProgramRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AcademicProgramRepository(ApplicationDbContext context, IMapper mapper)
        {

            _context = context;
            _mapper = mapper;
        }
        public async Task<AcademicProgram?> CreateAsync(CreateAcademicProgram dto)
        {
            var academicProgram = _mapper.Map<AcademicProgram>(dto);
            await _context.AcademicPrograms.AddAsync(academicProgram);

            var result = await _context.SaveChangesAsync();

            if (result > 0)
                return academicProgram;

            return null;
        }

        public async Task<string?> DeleteAsync(string Id)
        {
            var exist = await _context.AcademicPrograms.AnyAsync(a => a.Id == Id);

            if (!exist)
                return "Academic Program Not Found";

            var academicProgram = new AcademicProgram() { Id = Id };

            _context.Remove(academicProgram);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
                return null;

            return "Save Change Failed";
        }

        public async Task<IEnumerable<AcademicProgram>?> GetAllAsync()
        {
            return await _context.AcademicPrograms.ToListAsync();
        }

        public async Task<GetAcademicProgram?> GetByIdAsync(string Id)
        {
            return await _context.AcademicPrograms
                                    .Where(a => a.Id == Id)
                                    .Select(a => new GetAcademicProgram
                                    {

                                        Id = a.Id,
                                        Name = a.Name,
                                        Description = a.Description,
                                        Committees = (ICollection<GetLecturerResult>)a.Committees!.Select(c => new GetLecturerResult{
                                            UserName = c.Lecturer!.AppUser!.UserName,
                                            StaffId = c.Lecturer!.StaffId,
                                            PhoneNumber = c.Lecturer!.AppUser.PhoneNumber,
                                            Email = c.Lecturer!.AppUser.Email,
                                            Domain = c.Lecturer!.Domain
                                        })
                                    }).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<GetLecturerResult>?> GetLecturerNotCommittee(string Id)
    {
        return await _context.Lecturers
                                .Where(l => l.Committee.FirstOrDefault(c => c.AcademicProgramId == Id) == null)
                                .Select(l => new GetLecturerResult
                                {

                                    UserName = l.AppUser.UserName,
                                    StaffId = l.StaffId,
                                    PhoneNumber = l.AppUser.PhoneNumber,
                                    Email = l.AppUser.Email,
                                    Domain = l.Domain
                                }).AsNoTracking().ToListAsync();
    }
}
}