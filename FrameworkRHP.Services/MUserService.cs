using FrameworkRHP.Core.Models.EF;
using FrameworkRHP.Infrastructure.Context;
using FrameworkRHP.Infrastructure.Repository.Core;
using FrameworkRHP.Infrastructure.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace FrameworkRHP.Services
{
    public class UserRepository : GenericRepository<Muser>, IMUserRepository
    {
        public UserRepository(EProcurementDbContext context) : base(context) { } 
          
        public async Task<Muser?> GetMuserByIdAsync(int ParamUserId)
        {
            var User = await _context.Musers.Where(x => x.Intuserid == ParamUserId).Include(e => e.Muserroles).FirstOrDefaultAsync();
            return User;
        }
         
    }
     
}
