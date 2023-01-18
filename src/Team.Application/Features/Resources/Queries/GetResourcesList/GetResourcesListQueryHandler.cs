using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Team.Application.Contracts.Persistence;
using Team.Application.Dtos;

namespace Team.Application.Features.Resources.Queries.GetResourcesList
{
    public class GetResourcesListQueryHandler : IRequestHandler<GetResourcesListQuery, IEnumerable<ResourceDto>>
    {
        private readonly IResourceRepository _resourceRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetResourcesListQueryHandler> _logger;

        public GetResourcesListQueryHandler(IResourceRepository resourceRepository, IMapper mapper, ILogger<GetResourcesListQueryHandler> logger)
        {
            _resourceRepository = resourceRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<ResourceDto>> Handle(GetResourcesListQuery request, CancellationToken cancellationToken)
        {
            var resultList = await _resourceRepository.GetAllAsync();

            return _mapper.Map<List<ResourceDto>>(resultList);

        }
    }
}
