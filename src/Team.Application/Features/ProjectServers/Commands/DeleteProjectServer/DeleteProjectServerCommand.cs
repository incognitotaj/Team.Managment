using MediatR;

namespace Team.Application.Features.ProjectServers.Commands.DeleteProjectServer
{
    public class DeleteProjectServerCommand : IRequest
    {
        public Guid ProjectId { get; set; }
        public Guid ProjectServerId { get; set; }
    }
}
