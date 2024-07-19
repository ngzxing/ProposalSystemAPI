using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProposalSystem.Dtos.Admin;

namespace ProposalSystem.Interfaces
{
    public interface IAdminRepository
    {
        public Task<GetAdmin?> GetByIdAsync(string Id);
    }
}