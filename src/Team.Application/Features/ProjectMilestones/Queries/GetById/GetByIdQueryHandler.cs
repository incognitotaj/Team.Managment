using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Team.Application.Contracts.Persistence;
using Team.Application.Dtos;
using Team.Application.Exceptions;
using Team.Domain.Entities;

namespace Team.Application.Features.ProjectMilestones.Queries.GetById
{
    public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, ProjectMilestoneDto>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectMilestoneRepository _projectMilestoneRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetByIdQueryHandler> _logger;

        public GetByIdQueryHandler(IProjectRepository projectRepository, IProjectMilestoneRepository projectMilestoneRepository, IMapper mapper, ILogger<GetByIdQueryHandler> logger)
        {
            _projectRepository = projectRepository;
            _projectMilestoneRepository = projectMilestoneRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ProjectMilestoneDto> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var entityProject = await _projectRepository.GetByIdAsync(request.ProjectId);
            if (entityProject == null)
            {
                _logger.LogError($"Error: Project does not exist");
                throw new NotFoundException(nameof(Project), request.ProjectId);
            }

            var entity = await _projectMilestoneRepository.GetByIdAsync(request.ProjectMilestoneId);
            if (entity == null)
            {
                _logger.LogError($"Error: Project milestone does not exist");
                throw new NotFoundException(nameof(ProjectMilestone), request.ProjectMilestoneId);
            }

            return _mapper.Map<ProjectMilestoneDto>(entity);
        }
    }
}
