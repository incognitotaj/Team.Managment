using MediatR;
using Team.Application.Dtos;

namespace Team.Application.Features.ProjectResourceDailyTasks.Queries.GetById
{
    public class GetByIdQuery : IRequest<ProjectResourceDailyTaskDto>
    {
        public Guid ProjectResourceDailyTaskId { get; set; }
        public Guid ProjectResourceId { get; set; }
    }
}
