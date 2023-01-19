using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Team.Application.Contracts.Persistence;
using Team.Application.Exceptions;
using Team.Domain.Entities;

namespace Team.Application.Features.ProjectServers.Commands.UpdateProjectServer
{
    public class UpdateProjectServerCommandHandler : IRequestHandler<UpdateProjectServerCommand>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectServerRepository _projectServerRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateProjectServerCommandHandler> _logger;

        public UpdateProjectServerCommandHandler(IProjectRepository projectRepository, IProjectServerRepository projectServerRepository, IMapper mapper, ILogger<UpdateProjectServerCommandHandler> logger)
        {
            _projectRepository = projectRepository;
            _projectServerRepository = projectServerRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateProjectServerCommand request, CancellationToken cancellationToken)
        {
            var entityProject = await _projectRepository.GetByIdAsync(request.ProjectId);
            if (entityProject == null)
            {
                _logger.LogError($"Error: Project does not exist");
                throw new NotFoundException(nameof(Project), request.ProjectId);
            }

            var entityToUpdate = await _projectServerRepository.GetByIdAsync(request.ProjectServerId);
            if (entityToUpdate == null)
            {
                _logger.LogError($"Error: Project Server does not exist");
                throw new NotFoundException(nameof(ProjectServer), request.ProjectServerId);
            }

            _mapper.Map(request, entityToUpdate, typeof(UpdateProjectServerCommand), typeof(ProjectServer));

            await _projectServerRepository.UpdateAsync(entityToUpdate);

            _logger.LogInformation($"Project Server {entityToUpdate.Id} successfully updated");

            return Unit.Value;
        }
    }
}
