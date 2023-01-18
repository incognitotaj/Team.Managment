using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Team.Application.Contracts.Persistence;
using Team.Application.Exceptions;
using Team.Domain.Entities;

namespace Team.Application.Features.Resources.Commands.DeleteResource
{
    public class DeleteResourceCommandHandler : IRequestHandler<DeleteResourceCommand>
    {
        private readonly IResourceRepository _resourceRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteResourceCommandHandler> _logger;

        public DeleteResourceCommandHandler(IResourceRepository resourceRepository, IMapper mapper, ILogger<DeleteResourceCommandHandler> logger)
        {
            _resourceRepository = resourceRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteResourceCommand request, CancellationToken cancellationToken)
        {
            var entityToDelete = await _resourceRepository.GetByIdAsync(request.ResourceId);
            if (entityToDelete == null)
            {
                _logger.LogError($"Error: Resource does not exist");
                throw new NotFoundException(nameof(Resource), request.ResourceId);
            }

            await _resourceRepository.DeleteAsync(entityToDelete);
            _logger.LogInformation($"Resource {entityToDelete.Id} successfully deleted");

            return Unit.Value;
        }
    }
}
