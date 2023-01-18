using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Team.Application.Contracts.Persistence;
using Team.Application.Exceptions;
using Team.Domain.Entities;

namespace Team.Application.Features.ProjectMilestones.Commands.CreateProjectMilestone
{
    public class CreateProjectMilestoneCommandHandler : IRequestHandler<CreateProjectMilestoneCommand, Guid>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectMilestoneRepository _projectMilestoneRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateProjectMilestoneCommandHandler> _logger;

        public CreateProjectMilestoneCommandHandler(IProjectRepository projectRepository, IProjectMilestoneRepository projectMilestoneRepository, IMapper mapper, ILogger<CreateProjectMilestoneCommandHandler> logger)
        {
            _projectRepository = projectRepository;
            _projectMilestoneRepository = projectMilestoneRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Guid> Handle(CreateProjectMilestoneCommand request, CancellationToken cancellationToken)
        {
            var entityProject = await _projectRepository.GetByIdAsync(request.ProjectId);
            if (entityProject == null)
            {
                _logger.LogError($"Error: Project does not exist");
                throw new NotFoundException(nameof(Project), request.ProjectId);
            }

            var entity = _mapper.Map<ProjectMilestone>(request);

            var newEntity = await _projectMilestoneRepository.AddAsync(entity);

            _logger.LogInformation($"Project client {newEntity.Id} created successfully on {newEntity.CreatedOn}");

            return newEntity.Id;
        }
    }
}
