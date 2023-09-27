using SchoolProject.Domain.Entities;

namespace SchoolProject.Domain.IRepositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetById(object id);
        Task<List<T>> GetAll();
        Task Add(T entity);
    }
}