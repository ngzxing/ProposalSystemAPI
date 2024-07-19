using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ProposalSystem.Data;
using ProposalSystem.Dtos;
using ProposalSystem.Dtos.Student;
using ProposalSystem.Models;

namespace ProposalSystem.Interfaces
{
    public interface IStudentRepository
    {
        Task<List<GetStudentResult>> GetAllAsync( QueryStudent? query );
        Task<GetStudentResult?> GetByIdAsync(string Id);
        Task<Student?> GetSimpleByIdAsync(string Id);
        Task< (bool, Object) > CreateAsync( UserManager<AppUser> userManager,  Student dto, string password);
        Task<UpdateStudentResult?> UpdateAsync(string Id, UserManager<AppUser> userManager, UpdateStudent dto);
        Task<string?> DeleteAsync(string Id, UserManager<AppUser> userManager);

        Task< (GetStudentResult?, string)> AddSupervisorAsync(string studentId, UpdateSupervisor dto );

        Task<string?> DeleteSupervisorAsync(string studentId, string dto );

        Task<(GetStudentResult?, string)> AddEvaluatorAsync(string studentId, UpdateEvaluator dto);

        Task<string?> DeleteEvaluatorAsync(string studentId, string staffId);
    }
}