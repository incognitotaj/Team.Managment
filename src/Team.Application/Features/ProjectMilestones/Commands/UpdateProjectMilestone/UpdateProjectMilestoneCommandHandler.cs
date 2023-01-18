using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Team.Application.Contracts.Persistence;
using Team.Application.Exceptions;
using Team.Domain.Entities;

namespace Team.Application.Features.ProjectMilestones.Commands.UpdateProjectMilestone
{
    public class UpdateProjectMilestoneCommandHandler : IRequestHandler<UpdateProjectMilestoneCommand>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectMilestoneRepository _projectMilestoneRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateProjectMilestoneCommandHandler> _logger;

        public UpdateProjectMilestoneCommandHandler(IProjectRepository projectRepository, IProjectMilestoneRepository projectMilestoneRepository, IMapper mapper, ILogger<UpdateProjectMilestoneCommandHandler> logger)
        {
            _projectRepository = projectRepository;
            _projectMilestoneRepository = projectMilestoneRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateProjectMilestoneCommand request, CancellationToken cancellationToken)
        {
            var entityProject = await _projectRepository.GetByIdAsync(request.ProjectId);
            if (entityProject == null)
            {
                _logger.LogError($"Error: Project does not exist");
                throw new NotFoundException(nameof(Project), request.ProjectId);
            }

            var entityToUpdate = await _projectMilestoneRepository.GetByIdAsync(request.ProjectMilestoneId);
            if (entityToUpdate == null)
            {
                _logger.LogError($"Error: Project milestone does not exist");
                throw new NotFoundException(nameof(ProjectMilestone), request.ProjectMilestoneId);
            }

            _mapper.Map(request, entityToUpdate, typeof(UpdateProjectMilestoneCommand), typeof(ProjectMilestone));

            await _projectMilestoneRepository.UpdateAsync(entityToUpdate);

            _logger.LogInformation($"Project milestone {entityToUpdate.Id} successfully updated");

            return Unit.Value;
        }
    }
}
