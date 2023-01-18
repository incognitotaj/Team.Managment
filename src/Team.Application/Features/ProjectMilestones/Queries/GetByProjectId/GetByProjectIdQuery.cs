using MediatR;
using Team.Application.Dtos;

namespace Team.Application.Features.ProjectMilestones.Queries.GetByProjectId
{
    public class GetByProjectIdQuery : IRequest<IEnumerable<ProjectMilestoneDto>>
    {
        public Guid ProjectId { get; set; }
    }
}
