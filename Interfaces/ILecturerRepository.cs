using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ProposalSystem.Dtos.Lecturer;
using ProposalSystem.Models;

namespace ProposalSystem.Interfaces
{
    public interface ILecturerRepository
    {
        Task<List<GetLecturerResult>> GetAllAsync( QueryLecturer? query );
        Task<GetLecturerResult?> GetByIdAsync(string Id);
        Task< (CreateLecturerResult?, string) > CreateAsync( UserManager<AppUser> userManager,  Lecturer dto, string password);
        Task<UpdateLecturerResult?> UpdateAsync(string Id, UserManager<AppUser> userManager, UpdateLecturer dto);
        Task<string?> DeleteAsync(string Id, UserManager<AppUser> userManager);

        Task<bool> ExistAsync(string Id);

        
    }
}