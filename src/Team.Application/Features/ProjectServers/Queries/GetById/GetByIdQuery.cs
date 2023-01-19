using MediatR;
using Team.Application.Dtos;

namespace Team.Application.Features.ProjectServers.Queries.GetById
{
    public class GetByIdQuery : IRequest<ProjectServerDto>
    {
        public Guid ProjectId { get; set; }
        public Guid ProjectServerId { get; set; }
    }
}
