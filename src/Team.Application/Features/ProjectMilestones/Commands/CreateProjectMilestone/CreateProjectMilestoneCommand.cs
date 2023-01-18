using MediatR;

namespace Team.Application.Features.ProjectMilestones.Commands.CreateProjectMilestone
{
    public class CreateProjectMilestoneCommand : IRequest<Guid>
    {
        public Guid ProjectId { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
