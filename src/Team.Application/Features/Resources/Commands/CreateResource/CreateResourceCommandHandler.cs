using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Team.Application.Contracts.Persistence;
using Team.Domain.Entities;

namespace Team.Application.Features.Resources.Commands.CreateResource
{
    public class CreateResourceCommandHandler : IRequestHandler<CreateResourceCommand, Guid>
    {
        private readonly IResourceRepository _resourceRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateResourceCommandHandler> _logger;

        public CreateResourceCommandHandler(IResourceRepository resourceRepository, IMapper mapper, ILogger<CreateResourceCommandHandler> logger)
        {
            _resourceRepository = resourceRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Guid> Handle(CreateResourceCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Resource>(request);

            var newEntity = await _resourceRepository.AddAsync(entity);
            _logger.LogInformation($"Resource {newEntity.Id} created successfully on {newEntity.CreatedOn}");

            return newEntity.Id;

        }
    }
}
