using MediatR;

namespace Team.Application.Features.ProjectMilestones.Commands.UpdateProjectMilestone
{
    public class UpdateProjectMilestoneCommand : IRequest
    {
        public Guid ProjectMilestoneId { get; set; }
        public Guid ProjectId { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
