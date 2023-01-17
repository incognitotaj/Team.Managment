using MediatR;
using Team.Application.Dtos;

namespace Team.Application.Features.Resources.Queries.GetResourcesList
{
    public class GetResourcesListQuery : IRequest<IEnumerable<ResourceDto>>
    {
    }
}
