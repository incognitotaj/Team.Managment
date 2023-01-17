using MediatR;
using Team.Application.Dtos;

namespace Team.Application.Features.Resources.Queries.GetResourceById
{
    public class GetResourceByIdQuery : IRequest<ResourceDto>
    {
        public Guid ResourceId { get; set; }
    }
}
