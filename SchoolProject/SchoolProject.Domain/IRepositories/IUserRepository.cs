using SchoolProject.Domain.Entities;

namespace SchoolProject.Domain.IRepositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByNameAsync(string login);
    Task<User?> GetByClassAsync(string classroom);
}