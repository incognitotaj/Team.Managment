using Microsoft.Extensions.DependencyInjection;
using Team.Application.Contracts.Persistence;
using Team.Application.Contracts.Services;
using Team.Infrastructure.Repositories;
using Team.Infrastructure.Services;

namespace Team.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));

            services.AddScoped<IResourceRepository, ResourceRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IProjectClientRepository, ProjectClientRepository>();
            services.AddScoped<IProjectMilestoneRepository, ProjectMilestoneRepository>();
            services.AddScoped<IProjectDocumentRepository, ProjectDocumentRepository>();
            services.AddScoped<IProjectResourceRepository, ProjectResourceRepository>();


            services.AddTransient<IFileUploadOnServerService, FileUploadOnServerService>();

            return services;
        }
    }
}
