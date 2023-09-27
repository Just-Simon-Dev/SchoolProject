using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Domain.IRepositories;
using SchoolProject.Infrastructure.Repositories;

namespace SchoolProject.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // add services
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IClassroomRepository, ClassroomRepository>();
            services.AddScoped<IEvidentionRepository, EvidentionRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            return services;
        }
    }
}
