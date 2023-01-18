using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Team.Application.Contracts.Persistence;
using Team.Application.Exceptions;
using Team.Domain.Entities;

namespace Team.Application.Features.ProjectResources.Commands.DeleteProjectResource
{
    public class DeleteProjectResourceCommandHandler : IRequestHandler<DeleteProjectResourceCommand>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectResourceRepository _projectResourceRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteProjectResourceCommandHandler> _logger;

        public DeleteProjectResourceCommandHandler(IProjectRepository projectRepository, IProjectResourceRepository projectResourceRepository, IMapper mapper, ILogger<DeleteProjectResourceCommandHandler> logger)
        {
            _projectRepository = projectRepository;
            _projectResourceRepository = projectResourceRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteProjectResourceCommand request, CancellationToken cancellationToken)
        {
            var entityProject = await _projectRepository.GetByIdAsync(request.ProjectId);
            if (entityProject == null)
            {
                _logger.LogError($"Error: Project does not exist");
                throw new NotFoundException(nameof(Project), request.ProjectId);
            }

            var entityToDelete = await _projectResourceRepository.GetByIdAsync(request.ProjectResourceId);
            if (entityToDelete == null)
            {
                _logger.LogError($"Error: Project resource does not exist");
                throw new NotFoundException(nameof(ProjectClient), request.ProjectResourceId);
            }

            await _projectResourceRepository.DeleteAsync(entityToDelete);
            _logger.LogInformation($"Project resource {entityToDelete.Id} successfully deleted");

            return Unit.Value;
        }
    }
}
