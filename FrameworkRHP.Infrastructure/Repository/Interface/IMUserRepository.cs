using FrameworkRHP.Core.Models.EF;
using FrameworkRHP.Infrastructure.Repository.Core;

namespace FrameworkRHP.Infrastructure.Repository.Interface
{
    public interface IMUserRepository : IGenericRepository<Muser>
    {
        Task<Muser?> GetMuserByIdAsync(int ParamUserId);
    }
}
