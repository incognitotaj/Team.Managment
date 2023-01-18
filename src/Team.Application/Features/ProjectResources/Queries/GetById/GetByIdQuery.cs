using MediatR;
using Team.Application.Dtos;

namespace Team.Application.Features.ProjectResources.Queries.GetById
{
    public class GetByIdQuery : IRequest<ProjectResourceDto>
    {
        public Guid ProjectId { get; set; }
        public Guid ProjectResourceId { get; set; }
    }
}
