namespace SchoolProject.Domain.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        Task commit();
        void rollback();
        IRepository<T> Repository<T>() where T : class;
    }
}
