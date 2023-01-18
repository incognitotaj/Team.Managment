using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Team.Application.Contracts.Persistence;
using Team.Application.Exceptions;
using Team.Domain.Entities;

namespace Team.Application.Features.ProjectMilestones.Commands.DeleteProjectMilestone
{
    public class DeleteProjectMilestoneCommandHandler : IRequestHandler<DeleteProjectMilestoneCommand>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectMilestoneRepository _projectMilestoneRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteProjectMilestoneCommandHandler> _logger;

        public DeleteProjectMilestoneCommandHandler(IProjectRepository projectRepository, IProjectMilestoneRepository projectMilestoneRepository, IMapper mapper, ILogger<DeleteProjectMilestoneCommandHandler> logger)
        {
            _projectRepository = projectRepository;
            _projectMilestoneRepository = projectMilestoneRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteProjectMilestoneCommand request, CancellationToken cancellationToken)
        {
            var entityProject = await _projectRepository.GetByIdAsync(request.ProjectId);
            if (entityProject == null)
            {
                _logger.LogError($"Error: Project does not exist");
                throw new NotFoundException(nameof(Project), request.ProjectId);
            }

            var entityToDelete = await _projectMilestoneRepository.GetByIdAsync(request.ProjectMilestoneId);
            if (entityToDelete == null)
            {
                _logger.LogError($"Error: Project resource does not exist");
                throw new NotFoundException(nameof(ProjectMilestone), request.ProjectMilestoneId);
            }

            await _projectMilestoneRepository.DeleteAsync(entityToDelete);
            _logger.LogInformation($"Project resource {entityToDelete.Id} successfully deleted");

            return Unit.Value;
        }
    }
}
