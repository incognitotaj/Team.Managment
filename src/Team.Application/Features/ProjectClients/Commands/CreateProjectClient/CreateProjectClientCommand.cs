using MediatR;

namespace Team.Application.Features.ProjectClients.Commands.CreateProjectClient
{
    public class CreateProjectClientCommand : IRequest<Guid>
    {
        public Guid ProjectId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
