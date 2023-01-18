using MediatR;

namespace Team.Application.Features.ProjectResources.Commands.DeleteProjectResource
{
    public class DeleteProjectResourceCommand : IRequest
    {
        public Guid ProjectResourceId { get; set; }
        public Guid ProjectId { get; set; }
    }
}
