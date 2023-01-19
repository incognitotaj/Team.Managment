using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Team.Application.Contracts.Persistence;
using Team.Application.Dtos;
using Team.Application.Exceptions;
using Team.Domain.Entities;

namespace Team.Application.Features.ProjectResourceDailyTasks.Queries.GetByProjectResourceId
{
    public class GetByProjectResourceIdQueryHandler : IRequestHandler<GetByProjectResourceIdQuery, IEnumerable<ProjectResourceDailyTaskDto>>
    {
        private readonly IProjectResourceRepository _projectResourceRepository;
        private readonly IProjectResourceDailyTaskRepository _projectResourceDailyTaskRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetByProjectResourceIdQueryHandler> _logger;

        public GetByProjectResourceIdQueryHandler(IProjectResourceRepository projectResourceRepository, IProjectResourceDailyTaskRepository projectResourceDailyTaskRepository, IMapper mapper, ILogger<GetByProjectResourceIdQueryHandler> logger)
        {
            _projectResourceRepository = projectResourceRepository;
            _projectResourceDailyTaskRepository = projectResourceDailyTaskRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<ProjectResourceDailyTaskDto>> Handle(GetByProjectResourceIdQuery request, CancellationToken cancellationToken)
        {
            var entityProjectResource = await _projectResourceRepository.GetByIdAsync(request.ProjectResourceId);
            if (entityProjectResource == null)
            {
                _logger.LogError($"Error: Project resource does not exist");
                throw new NotFoundException(nameof(ProjectResource), request.ProjectResourceId);
            }

            var projectResourceDailyTasks = await _projectResourceDailyTaskRepository.GetAsync(p => p.ProjectResourceId == request.ProjectResourceId);

            return _mapper.Map<IEnumerable<ProjectResourceDailyTaskDto>>(projectResourceDailyTasks);
        }
    }
}
