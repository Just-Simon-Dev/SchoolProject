using Microsoft.EntityFrameworkCore;
using SchoolProject.Domain.IRepositories;
using SchoolProject.Infrastructure.Configuration.Repositories;
using SchoolProject.Infrastructure.Context;

namespace SchoolProject.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly SchoolProjectContext _dbContext;
        private bool _disposed;
        public UnitOfWork(SchoolProjectContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task commit()
        {
            await _dbContext.SaveChangesAsync();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public IRepository<T> Repository<T>() where T : class
        {
            return new Repository<T>(_dbContext);
        }

        public void rollback()
        {
            foreach (var entry in _dbContext.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                }
            }
        }
    }
}
