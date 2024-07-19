using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProposalSystem.Dtos.AcademicProgram;
using ProposalSystem.Dtos.Lecturer;
using ProposalSystem.Models;

namespace ProposalSystem.Interfaces
{
    public interface IAcademicProgramRepository
    {
        Task<GetAcademicProgram?> GetByIdAsync( string Id);
        Task<IEnumerable<AcademicProgram>?> GetAllAsync();
        Task<AcademicProgram?> CreateAsync(CreateAcademicProgram dto);

        public Task<IEnumerable<GetLecturerResult>?> GetLecturerNotCommittee(string Id);

        Task<string?> DeleteAsync(string Id);
    }
}