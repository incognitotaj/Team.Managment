using MediatR;
using Team.Application.Dtos;

namespace Team.Application.Features.ProjectClients.Queries.GetByProjectId
{
    public class GetByProjectIdQuery : IRequest<IEnumerable<ProjectClientDto>>
    {
        public Guid ProjectId { get; set; }
    }
}
