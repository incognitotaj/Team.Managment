using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Team.Application.Contracts.Persistence;
using Team.Application.Dtos;
using Team.Application.Exceptions;
using Team.Domain.Entities;

namespace Team.Application.Features.Resources.Queries.GetResourceById
{
    public class GetResourceByIdQueryHandler : IRequestHandler<GetResourceByIdQuery, ResourceDto>
    {
        private readonly IResourceRepository _resourceRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetResourceByIdQueryHandler> _logger;

        public GetResourceByIdQueryHandler(IResourceRepository resourceRepository, IMapper mapper, ILogger<GetResourceByIdQueryHandler> logger)
        {
            _resourceRepository = resourceRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ResourceDto> Handle(GetResourceByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _resourceRepository.GetByIdAsync(request.ResourceId);
            if (entity == null)
            {
                _logger.LogError($"Error: Resource does not exist");
                throw new NotFoundException(nameof(Resource), request.ResourceId);
            }

            return _mapper.Map<ResourceDto>(entity);
        }
    }
}
