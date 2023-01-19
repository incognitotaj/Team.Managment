using MediatR;

namespace Team.Application.Features.PrjectResourceDailyTasks.Commands.DeleteProjectResourceDailyTask
{
    public class DeleteProjectResourceDailyTaskCommand : IRequest
    {
        public Guid ProjectResourceDailyTaskId { get; set; }
        public Guid ProjectResourceId { get; set; }
    }
}
