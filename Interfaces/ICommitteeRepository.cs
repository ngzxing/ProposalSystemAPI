using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProposalSystem.Dtos.AcademicProgram;
using ProposalSystem.Dtos.Committee;
using ProposalSystem.Dtos.Lecturer;

namespace ProposalSystem.Interfaces
{
    public interface ICommitteeRepository
    {
        public Task<IEnumerable<GetCommitteeResult>?> GetByIdAsync(string Id);
        public Task<(GetCommitteeResult?, string)> CreateAsync(CreateCommittee dto);
    
        public Task<string?> DeleteAsync(string Id);
        public Task<ICollection<GetAcademicProgram>?> GetProgramsByLecturerId(string Id);

    }
}