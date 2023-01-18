using MediatR;
using Team.Application.Dtos;

namespace Team.Application.Features.ProjectMilestones.Queries.GetById
{
    public class GetByIdQuery : IRequest<ProjectMilestoneDto>
    {
        public Guid ProjectId { get; set; }
        public Guid ProjectMilestoneId { get; set; }
    }
}
