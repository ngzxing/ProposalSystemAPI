using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProposalSystem.Data;
using ProposalSystem.Dtos.Admin;
using ProposalSystem.Interfaces;

namespace ProposalSystem.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationDbContext _context;
        public AdminRepository( ApplicationDbContext context ){

            _context = context;
        }
        public async Task<GetAdmin?> GetByIdAsync(string Id)
        {
            return await _context.Admin.Where(a => a.AdminId == Id).Select( a => new GetAdmin{

                UserName = a.AppUser!.UserName,
                AdminId = a.AdminId,
                Email = a.AppUser.Email,
                Id = a.AppUser!.Id
                }
                ).FirstOrDefaultAsync( );
        }
    }
}