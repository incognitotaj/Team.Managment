using MediatR;
using Team.Application.Dtos;

namespace Team.Application.Features.ProjectClients.Queries.GetById
{
    public class GetByIdQuery : IRequest<ProjectClientDto>
    {
        public Guid ProjectId { get; set; }
        public Guid ProjectClientId { get; set; }
    }
}
