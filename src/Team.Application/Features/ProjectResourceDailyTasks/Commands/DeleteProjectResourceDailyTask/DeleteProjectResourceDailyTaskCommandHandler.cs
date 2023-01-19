using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Team.Application.Contracts.Persistence;
using Team.Application.Exceptions;
using Team.Application.Features.PrjectResourceDailyTasks.Commands.DeleteProjectResourceDailyTask;
using Team.Domain.Entities;

namespace Team.Application.Features.ProjectResourceDailyTasks.Commands.DeleteProjectResourceDailyTask
{
    public class DeleteProjectResourceDailyTaskCommandHandler : IRequestHandler<DeleteProjectResourceDailyTaskCommand>
    {
        private readonly IProjectResourceRepository _projectResourceRepository;
        private readonly IProjectResourceDailyTaskRepository _projectResourceDailyTaskRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteProjectResourceDailyTaskCommandHandler> _logger;

        public DeleteProjectResourceDailyTaskCommandHandler(IProjectResourceRepository projectResourceRepository, IProjectResourceDailyTaskRepository projectResourceDailyTaskRepository, IMapper mapper, ILogger<DeleteProjectResourceDailyTaskCommandHandler> logger)
        {
            _projectResourceRepository = projectResourceRepository;
            _projectResourceDailyTaskRepository = projectResourceDailyTaskRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteProjectResourceDailyTaskCommand request, CancellationToken cancellationToken)
        {
            var entityProjectResource = await _projectResourceRepository.GetByIdAsync(request.ProjectResourceId);
            if (entityProjectResource == null)
            {
                _logger.LogError($"Error: Project resource does not exist");
                throw new NotFoundException(nameof(ProjectResource), request.ProjectResourceId);
            }

            var entityToDelete = await _projectResourceDailyTaskRepository.GetByIdAsync(request.ProjectResourceDailyTaskId);
            if (entityToDelete == null)
            {
                _logger.LogError($"Error: Project resource daily task does not exist");
                throw new NotFoundException(nameof(ProjectResourceDailyTask), request.ProjectResourceDailyTaskId);
            }

            await _projectResourceDailyTaskRepository.DeleteAsync(entityToDelete);
            _logger.LogInformation($"Project resource daily task {entityToDelete.Id} successfully deleted");

            return Unit.Value;
        }
    }
}
