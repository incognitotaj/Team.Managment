using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Team.Application.Contracts.Persistence;
using Team.Application.Exceptions;
using Team.Domain.Entities;

namespace Team.Application.Features.ProjectResourceDailyTasks.Commands.UpdateProjectResourceDailyTask
{
    public class UpdateProjectResourceDailyTaskCommandHandler : IRequestHandler<UpdateProjectResourceDailyTaskCommand>
    {
        private readonly IProjectResourceRepository _projectResourceRepository;
        private readonly IProjectResourceDailyTaskRepository _projectResourceDailyTaskRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateProjectResourceDailyTaskCommandHandler> _logger;

        public UpdateProjectResourceDailyTaskCommandHandler(IProjectResourceRepository projectResourceRepository, IProjectResourceDailyTaskRepository projectResourceDailyTaskRepository, IMapper mapper, ILogger<UpdateProjectResourceDailyTaskCommandHandler> logger)
        {
            _projectResourceRepository = projectResourceRepository;
            _projectResourceDailyTaskRepository = projectResourceDailyTaskRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateProjectResourceDailyTaskCommand request, CancellationToken cancellationToken)
        {
            var entityProjectResource = await _projectResourceRepository.GetByIdAsync(request.ProjectResourceId);
            if (entityProjectResource == null)
            {
                _logger.LogError($"Error: Project resource does not exist");
                throw new NotFoundException(nameof(ProjectResource), request.ProjectResourceId);
            }

            var entityToUpdate = await _projectResourceDailyTaskRepository.GetByIdAsync(request.ProjectResourceDailyTaskId);
            if (entityToUpdate == null)
            {
                _logger.LogError($"Error: Project resource daily task does not exist");
                throw new NotFoundException(nameof(ProjectResourceDailyTask), request.ProjectResourceDailyTaskId);
            }

            _mapper.Map(request, entityToUpdate, typeof(UpdateProjectResourceDailyTaskCommand), typeof(ProjectResourceDailyTask));

            await _projectResourceDailyTaskRepository.UpdateAsync(entityToUpdate);

            _logger.LogInformation($"Project resource daily task {entityToUpdate.Id} successfully updated");

            return Unit.Value;
        }
    }
}
