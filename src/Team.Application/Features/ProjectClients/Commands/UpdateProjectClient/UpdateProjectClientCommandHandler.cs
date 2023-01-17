using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Team.Application.Contracts.Persistence;
using Team.Application.Exceptions;
using Team.Application.Features.ProjectClients.Commands.CreateProjectClient;
using Team.Domain.Entities;

namespace Team.Application.Features.ProjectClients.Commands.UpdateProjectClient
{
    public class UpdateProjectClientCommandHandler : IRequestHandler<UpdateProjectClientCommand>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectClientRepository _projectClientRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateProjectClientCommandHandler> _logger;

        public UpdateProjectClientCommandHandler(IProjectRepository projectRepository, IProjectClientRepository projectClientRepository, IMapper mapper, ILogger<UpdateProjectClientCommandHandler> logger)
        {
            _projectRepository = projectRepository;
            _projectClientRepository = projectClientRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateProjectClientCommand request, CancellationToken cancellationToken)
        {
            var entityProject = await _projectRepository.GetByIdAsync(request.ProjectId);
            if (entityProject == null)
            {
                _logger.LogError($"Error: Project does not exist");
                throw new NotFoundException(nameof(Project), request.ProjectId);
            }

            var entityToUpdate = await _projectClientRepository.GetByIdAsync(request.ProjectClientId);
            if (entityToUpdate == null)
            {
                _logger.LogError($"Error: Project client does not exist");
                throw new NotFoundException(nameof(ProjectClient), request.ProjectClientId);
            }

            _mapper.Map(request, entityToUpdate, typeof(UpdateProjectClientCommand), typeof(ProjectClient));

            await _projectClientRepository.UpdateAsync(entityToUpdate);

            _logger.LogInformation($"Project client {entityToUpdate.Id} successfully updated");

            return Unit.Value;
        }
    }
}
