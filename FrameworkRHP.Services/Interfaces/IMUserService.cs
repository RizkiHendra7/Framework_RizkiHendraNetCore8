using FrameworkRHP.Core.Interfaces.Core;
using FrameworkRHP.Core.Models.EF; 

namespace FrameworkRHP.Services.Interfaces
{
    public interface IMUserService : IGenericRepository<Muser>
    {
        Muser GetByUserName(string paramUserName);
    }
}
