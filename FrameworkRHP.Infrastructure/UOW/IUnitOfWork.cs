using FrameworkRHP.Core.Models.EF;
using FrameworkRHP.Infrastructure.Repository;

namespace FrameworkRHP.Infrastructure.UOW
{
    public interface IUnitOfWork
    {
        GenericRepository<Muser> MUsers { get; }
        GenericRepository<Mrole> MRoles { get; } 
        void CreateTransaction(); 
        void Commit(); 
        void Rollback(); 
        Task Save();
    } 
}
