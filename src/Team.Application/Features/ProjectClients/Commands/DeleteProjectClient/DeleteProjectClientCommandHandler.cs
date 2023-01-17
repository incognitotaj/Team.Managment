using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Team.Application.Contracts.Persistence;
using Team.Application.Exceptions;
using Team.Application.Features.ProjectClients.Commands.UpdateProjectClient;
using Team.Domain.Entities;

namespace Team.Application.Features.ProjectClients.Commands.DeleteProjectClient
{
    public class DeleteProjectClientCommandHandler : IRequestHandler<DeleteProjectClientCommand>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectClientRepository _projectClientRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateProjectClientCommandHandler> _logger;

        public DeleteProjectClientCommandHandler(IProjectRepository projectRepository, IProjectClientRepository projectClientRepository, IMapper mapper, ILogger<UpdateProjectClientCommandHandler> logger)
        {
            _projectRepository = projectRepository;
            _projectClientRepository = projectClientRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteProjectClientCommand request, CancellationToken cancellationToken)
        {
            var entityProject = await _projectRepository.GetByIdAsync(request.ProjectId);
            if (entityProject == null)
            {
                _logger.LogError($"Error: Project does not exist");
                throw new NotFoundException(nameof(Project), request.ProjectId);
            }

            var entityToDelete = await _projectClientRepository.GetByIdAsync(request.ProjectClientId);
            if (entityToDelete == null)
            {
                _logger.LogError($"Error: Project client does not exist");
                throw new NotFoundException(nameof(ProjectClient), request.ProjectClientId);
            }

            await _projectClientRepository.DeleteAsync(entityToDelete);
            _logger.LogInformation($"Project client {entityToDelete.Id} successfully deleted");

            return Unit.Value;
        }
    }
}
