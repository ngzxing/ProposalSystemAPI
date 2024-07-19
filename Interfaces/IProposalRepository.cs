using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProposalSystem.Dtos.Proposal;
using ProposalSystem.Dtos.Student;

namespace ProposalSystem.Interfaces
{
    public interface IProposalRepository
    {
        Task<(GetProposalResult?, string?)> CreateAsync( CreateProposal dto, string FileLink,string formLink, GetStudentResult student );
        Task<( ICollection<GetProposalResult>?, string? )> GetAllInfoAsync( QueryProposal dto );
        Task<GetProposalResult?> GetOneInfoAsync( string ProposalId );
        Task<string?> GetProposalLinkAsync(string ProposalId);
        Task<string?> GetFormLinkAsync(string ProposalId);
        Task<string?> UpdateStateAsync( string ProposalId, UpdateState dto );
        Task<string?> UpdateMarkAsync( string ProposalId, UpdateMark dto );
        Task<string?> DeleteAsync( string PorposalId );
    }
}