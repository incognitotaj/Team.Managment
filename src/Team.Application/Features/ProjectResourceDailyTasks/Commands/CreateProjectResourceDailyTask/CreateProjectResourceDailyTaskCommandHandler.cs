using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Team.Application.Contracts.Persistence;
using Team.Application.Exceptions;
using Team.Domain.Entities;

namespace Team.Application.Features.ProjectResourceDailyTasks.Commands.CreateProjectResourceDailyTask
{
    public class CreateProjectResourceDailyTaskCommandHandler : IRequestHandler<CreateProjectResourceDailyTaskCommand, Guid>
    {
        private readonly IProjectResourceRepository _projectResourceRepository;
        private readonly IProjectResourceDailyTaskRepository _projectResourceDailyTaskRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateProjectResourceDailyTaskCommandHandler> _logger;

        public CreateProjectResourceDailyTaskCommandHandler(IProjectResourceRepository projectResourceRepository, IProjectResourceDailyTaskRepository projectResourceDailyTaskRepository, IMapper mapper, ILogger<CreateProjectResourceDailyTaskCommandHandler> logger)
        {
            _projectResourceRepository = projectResourceRepository;
            _projectResourceDailyTaskRepository = projectResourceDailyTaskRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Guid> Handle(CreateProjectResourceDailyTaskCommand request, CancellationToken cancellationToken)
        {
            var entityProjectResource = await _projectResourceRepository.GetByIdAsync(request.ProjectResourceId);
            if (entityProjectResource == null)
            {
                _logger.LogError($"Error: Project resource does not exist");
                throw new NotFoundException(nameof(ProjectResource), request.ProjectResourceId);
            }

            var entity = _mapper.Map<ProjectResourceDailyTask>(request);

            var newEntity = await _projectResourceDailyTaskRepository.AddAsync(entity);

            _logger.LogInformation($"Project resource daily task {newEntity.Id} created successfully on {newEntity.CreatedOn}");

            return newEntity.Id;
        }
    }
}
