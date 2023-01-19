using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Team.Application.Contracts.Persistence;
using Team.Application.Exceptions;
using Team.Domain.Entities;

namespace Team.Application.Features.ProjectServers.Commands.DeleteProjectServer
{
    public class DeleteProjectServerCommandHandler : IRequestHandler<DeleteProjectServerCommand>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectServerRepository _projectServerRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteProjectServerCommandHandler> _logger;

        public DeleteProjectServerCommandHandler(IProjectRepository projectRepository, IProjectServerRepository projectServerRepository, IMapper mapper, ILogger<DeleteProjectServerCommandHandler> logger)
        {
            _projectRepository = projectRepository;
            _projectServerRepository = projectServerRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteProjectServerCommand request, CancellationToken cancellationToken)
        {
            var entityProject = await _projectRepository.GetByIdAsync(request.ProjectId);
            if (entityProject == null)
            {
                _logger.LogError($"Error: Project does not exist");
                throw new NotFoundException(nameof(Project), request.ProjectId);
            }

            var entityToDelete = await _projectServerRepository.GetByIdAsync(request.ProjectServerId);
            if (entityToDelete == null)
            {
                _logger.LogError($"Error: Project Server does not exist");
                throw new NotFoundException(nameof(ProjectServer), request.ProjectServerId);
            }

            await _projectServerRepository.DeleteAsync(entityToDelete);
            _logger.LogInformation($"Project Server {entityToDelete.Id} successfully deleted");

            return Unit.Value;
        }
    }
}
