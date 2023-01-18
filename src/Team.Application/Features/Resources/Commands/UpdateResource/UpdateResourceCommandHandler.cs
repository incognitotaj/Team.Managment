using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Team.Application.Contracts.Persistence;
using Team.Application.Exceptions;
using Team.Domain.Entities;

namespace Team.Application.Features.Resources.Commands.UpdateResource
{
    public class UpdateResourceCommandHandler : IRequestHandler<UpdateResourceCommand>
    {
        private readonly IResourceRepository _resourceRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateResourceCommandHandler> _logger;

        public UpdateResourceCommandHandler(IResourceRepository resourceRepository, IMapper mapper, ILogger<UpdateResourceCommandHandler> logger)
        {
            _resourceRepository = resourceRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateResourceCommand request, CancellationToken cancellationToken)
        {
            var entityToUpdate = await _resourceRepository.GetByIdAsync(request.ResourceId);
            if (entityToUpdate == null)
            {
                _logger.LogError($"Error: Resource does not exist");
                throw new NotFoundException(nameof(Resource), request.ResourceId);
            }

            _mapper.Map(request, entityToUpdate, typeof(UpdateResourceCommand), typeof(Resource));

            await _resourceRepository.UpdateAsync(entityToUpdate);

            _logger.LogInformation($"Resource {entityToUpdate.Id} successfully updated");

            return Unit.Value;
        }
    }
}
