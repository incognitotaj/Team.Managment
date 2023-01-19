using MediatR;

namespace Team.Application.Features.PrjectResourceDailyTasks.Commands.UpdateProjectResourceDailyTask
{
    public class UpdateProjectResourceDailyTaskCommand : IRequest
    {
        public Guid ProjectResourceDailyTaskId { get; set; }
        public Guid ProjectResourceId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int TaskStatus { get; set; }
    }
}
