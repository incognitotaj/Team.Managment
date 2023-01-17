using Team.Domain.Entities;

namespace Team.Application.Contracts.Persistence
{
    public interface IProjectClientRepository : IAsyncRepository<ProjectClient>
    {
        // Get Projects By User
        Task<IEnumerable<ProjectClient>> GetByProjectIdAsync(Guid projectId);
    }
}
