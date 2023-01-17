using Microsoft.Extensions.DependencyInjection;
using Team.Application.Contracts.Persistence;
using Team.Infrastructure.Repositories;

namespace Team.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));

            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IProjectClientRepository, ProjectClientRepository>();
            return services;
        }
    }
}
