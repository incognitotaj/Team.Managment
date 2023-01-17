using MediatR;

namespace Team.Application.Features.ProjectClients.Commands.UpdateProjectClient
{
    public class UpdateProjectClientCommand : IRequest
    {
        public Guid ProjectId { get; set; }
        public Guid ProjectClientId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
