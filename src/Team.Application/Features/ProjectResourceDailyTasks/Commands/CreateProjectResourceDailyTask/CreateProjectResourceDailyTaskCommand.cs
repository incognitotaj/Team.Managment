using MediatR;

namespace Team.Application.Features.ProjectResourceDailyTasks.Commands.CreateProjectResourceDailyTask
{
    public class CreateProjectResourceDailyTaskCommand : IRequest<Guid>
    {
        public Guid ProjectResourceId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int TaskStatus { get; set; }
    }
}
