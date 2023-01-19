using MediatR;

namespace Team.Application.Features.ProjectResources.Commands.CreateProjectResource
{
    public class CreateProjectResourceCommand : IRequest<Guid>
    {
        public Guid ProjectId { get; set; }
        public Guid ResourceId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
