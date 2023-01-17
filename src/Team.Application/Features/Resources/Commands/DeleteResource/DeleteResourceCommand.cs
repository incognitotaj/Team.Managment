using MediatR;

namespace Team.Application.Features.Resources.Commands.DeleteResource
{
    public class DeleteResourceCommand : IRequest
    {
        public Guid ResourceId { get; set; }
    }
}
