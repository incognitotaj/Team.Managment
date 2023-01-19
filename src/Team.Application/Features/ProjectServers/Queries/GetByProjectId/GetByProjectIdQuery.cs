using MediatR;
using Team.Application.Dtos;

namespace Team.Application.Features.ProjectServers.Queries.GetByProjectId
{
    public class GetByProjectIdQuery : IRequest<IEnumerable<ProjectServerDto>>
    {
        public Guid ProjectId { get; set; }
    }
}
