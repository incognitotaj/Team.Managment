using MediatR;
using Team.Application.Dtos;

namespace Team.Application.Features.ProjectResourceDailyTasks.Queries.GetByProjectResourceId
{
    public class GetByProjectResourceIdQuery : IRequest<IEnumerable<ProjectResourceDailyTaskDto>>
    {
        public Guid ProjectResourceId { get; set; }
    }
}
