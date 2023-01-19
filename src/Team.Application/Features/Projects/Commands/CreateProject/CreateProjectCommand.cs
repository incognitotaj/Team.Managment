using MediatR;

namespace Team.Application.Features.Projects.Commands.CreateProject
{
    public class CreateProjectCommand : IRequest<Guid>
    {
        public string ManagerId { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
