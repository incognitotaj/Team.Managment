using MediatR;

namespace Team.Application.Features.ProjectServers.Commands.CreateProjectServer
{
    public class CreateProjectServerCommand : IRequest<Guid>
    {
        public Guid ProjectId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
