using Microsoft.EntityFrameworkCore;
using SchoolProject.Domain.Entities;
using SchoolProject.Domain.Enums;
using SchoolProject.Domain.IRepositories;
using SchoolProject.Domain.SchoolProjectExceptions.Exceptions;
using SchoolProject.Infrastructure.Configuration.Repositories;
using SchoolProject.Infrastructure.Context;

namespace SchoolProject.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SchoolProjectContext _context;

        public UserRepository(SchoolProjectContext context)
        {
            _context = context;
        }
        public async Task<User> GetById(object id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                throw new UserNotFoundException();
            }
            return user;
        }

        public async Task<User?> GetByNameAsync(string login)
        {
            var users = await _context.Users.FirstOrDefaultAsync(user => user.Login == login);

            return users;
        }

        public async Task<User?> GetByClassAsync(string classroom)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Classroom.Name == classroom);
            
            if (user == null)
            {
                throw new UserNotFoundException();
            }

            return user;
        }

        public async Task<List<User>> GetAll()
        {
            var users = await _context.Users.ToListAsync();
            
            if (users == null || !users.Any())
            {
                throw new UserNotFoundException();
            }

            return users;
        }

        public async Task Add(User entity)
        {
            var result = await _context.Users.AddAsync(entity);
            if (result.State != EntityState.Added)
            {
                throw new EntityNotAddedException();
            }
        }
    }
}
