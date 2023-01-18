using MediatR;

namespace Team.Application.Features.ProjectMilestones.Commands.DeleteProjectMilestone
{
    public class DeleteProjectMilestoneCommand : IRequest
    {
        public Guid ProjectMilestoneId { get; set; }
        public Guid ProjectId { get; set; }
    }
}
