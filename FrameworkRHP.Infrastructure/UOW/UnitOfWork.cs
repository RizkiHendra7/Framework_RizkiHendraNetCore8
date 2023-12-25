using FrameworkRHP.Core.Models.EF;
using FrameworkRHP.Infrastructure.Context;
using FrameworkRHP.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworkRHP.Infrastructure.UOW
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public EProcurementDbContext Context = null;
        private IDbContextTransaction? _objTran = null;
        private bool _disposed;
        private string _errorMessage = string.Empty;


        public IUnitOfWork _unitOfWork;

        public GenericRepository<Muser> MUsers { get; private set; } 
        public GenericRepository<Mrole> MRoles { get; private set; } 

        public UnitOfWork(EProcurementDbContext _context)
        {
            Context = _context;
            MUsers = new GenericRepository<Muser>(Context);
            MRoles = new GenericRepository<Mrole>(Context);
        } 

        public void CreateTransaction()
        {
            _objTran = Context.Database.BeginTransaction();
        }
        public void Commit()
        {
            _objTran?.Commit();
        }
        public void Rollback()
        {
            _objTran?.Rollback();
            _objTran?.Dispose();
        }
        public async Task Save()
        {
            try
            {

                Context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                var exceptionEntries = ex.Entries.Where(e => e.State == EntityState.Added);
                foreach (var entry in exceptionEntries)
                {
                    foreach (var error in entry.Entity.GetType().GetProperties()
                        .Where(prop => !Attribute.IsDefined(prop, typeof(NotMappedAttribute)) &&
                                       prop.GetValue(entry.Entity) == null)
                        .Select(prop => new { Property = prop.Name }))
                    {
                        _errorMessage += $"Property: {error.Property} Error: Validation Failed {Environment.NewLine}";
                    }
                }
                _errorMessage = string.IsNullOrEmpty(_errorMessage) ? ex.Message : _errorMessage;
                throw new Exception(_errorMessage, ex);
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    Context.Dispose();
            _disposed = true;
        }
    }

    //public class UnitOfWork<TContext> : IUnitOfWork<TContext>, IDisposable where TContext : DbContext, new()
    //{
    //    public TContext Context { get; }
    //    private IDbContextTransaction _objTran;
    //    private bool _disposed;
    //    private string _errorMessage = string.Empty;

    //    public UnitOfWork(TContext _context)
    //    {
    //        Context = _context;
    //    }

    //    public void CreateTransaction()
    //    {
    //        _objTran = Context.Database.BeginTransaction();
    //    } 
    //    public void Commit()
    //    {
    //        _objTran.Commit();
    //    } 
    //    public void Rollback()
    //    {
    //        _objTran.Rollback();
    //        _objTran.Dispose();
    //    } 
    //    public void Save()
    //    {
    //        try
    //        {

    //            Context.SaveChanges();
    //        }
    //        catch (DbUpdateException ex)
    //        {
    //            var exceptionEntries = ex.Entries.Where(e => e.State == EntityState.Detached);
    //            foreach (var entry in exceptionEntries)
    //            {
    //                foreach (var error in entry.Entity.GetType().GetProperties()
    //                    .Where(prop => !Attribute.IsDefined(prop, typeof(NotMappedAttribute)) &&
    //                                   prop.GetValue(entry.Entity) == null)
    //                    .Select(prop => new { Property = prop.Name }))
    //                {
    //                    _errorMessage += $"Property: {error.Property} Error: Validation Failed {Environment.NewLine}";
    //                }
    //            }
    //            throw new Exception(_errorMessage, ex);
    //        }
    //    } 
    //    public void Dispose()
    //    {
    //        Dispose(true);
    //        GC.SuppressFinalize(this);
    //    }
    //    protected virtual void Dispose(bool disposing)
    //    {
    //        if (!_disposed)
    //            if (disposing)
    //                Context.Dispose();
    //        _disposed = true;
    //    }
    //}
}
