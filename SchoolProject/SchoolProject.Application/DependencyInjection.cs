using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Application.Hubs;
using SchoolProject.Application.services;

namespace SchoolProject.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IHashService, HashService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IEvidentionService, EvidentionService>();
            
            services.AddHttpContextAccessor();
            services.AddScoped<IUserContext, UserContext>();
            
            return services;
        }
    }
}
