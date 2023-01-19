using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Team.Application.Contracts.Persistence;
using Team.Application.Dtos;
using Team.Application.Exceptions;
using Team.Domain.Entities;

namespace Team.Application.Features.ProjectResourceDailyTasks.Queries.GetById
{
    public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, ProjectResourceDailyTaskDto>
    {
        private readonly IProjectResourceRepository _projectResourceRepository;
        private readonly IProjectResourceDailyTaskRepository _projectResourceDailyTaskRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetByIdQueryHandler> _logger;

        public GetByIdQueryHandler(IProjectResourceRepository projectResourceRepository, IProjectResourceDailyTaskRepository projectResourceDailyTaskRepository, IMapper mapper, ILogger<GetByIdQueryHandler> logger)
        {
            _projectResourceRepository = projectResourceRepository;
            _projectResourceDailyTaskRepository = projectResourceDailyTaskRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ProjectResourceDailyTaskDto> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var entityProjectResource = await _projectResourceRepository.GetByIdAsync(request.ProjectResourceId);
            if (entityProjectResource == null)
            {
                _logger.LogError($"Error: Project resource does not exist");
                throw new NotFoundException(nameof(ProjectResource), request.ProjectResourceId);
            }

            var entity = await _projectResourceDailyTaskRepository.GetByIdAsync(request.ProjectResourceDailyTaskId);
            if (entity == null)
            {
                _logger.LogError($"Error: Project resource daily task does not exist");
                throw new NotFoundException(nameof(ProjectResourceDailyTask), request.ProjectResourceDailyTaskId);
            }

            return _mapper.Map<ProjectResourceDailyTaskDto>(entity);
        }
    }
}
