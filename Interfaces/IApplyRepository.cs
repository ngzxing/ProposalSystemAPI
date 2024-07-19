using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProposalSystem.Dtos.ApplySupervisor;
using ProposalSystem.Models;

namespace ProposalSystem.Interfaces
{
    public interface IApplyRepository
    {
        Task<(ApplySupervisor?, string?)> CreateAsync( string MatricId, string StaffId );
        Task<string?> DeleteAsync(string ApplyId);
        Task<List<GetApplyResult>?> GetAllAsync(QueryApply dto);
        Task< GetApplyResult? > GetByIdAsync( string Id );
        Task< string? > ChangeStateAsync( string Id, ChangeState dto );
    }

    
}