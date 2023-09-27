using Microsoft.EntityFrameworkCore;
using SchoolProject.Domain.IRepositories;
using SchoolProject.Infrastructure.Context;
using System.Runtime.CompilerServices;
using SchoolProject.Domain.Entities;

namespace SchoolProject.Infrastructure.Configuration.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly SchoolProjectContext _dbContext;
        private readonly DbSet<T> _dbSet;
        public Repository(SchoolProjectContext context)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task Add(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task<List<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetById(object id)
        {
            return await _dbSet.FindAsync(id);
        }
    }
}