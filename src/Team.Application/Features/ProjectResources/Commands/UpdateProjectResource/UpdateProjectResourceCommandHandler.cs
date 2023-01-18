using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Team.Application.Contracts.Persistence;
using Team.Application.Exceptions;
using Team.Domain.Entities;

namespace Team.Application.Features.ProjectResources.Commands.UpdateProjectResource
{
    public class UpdateProjectResourceCommandHandler : IRequestHandler<UpdateProjectResourceCommand>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectResourceRepository _projectResourceRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateProjectResourceCommandHandler> _logger;

        public UpdateProjectResourceCommandHandler(IProjectRepository projectRepository, IProjectResourceRepository projectResourceRepository, IMapper mapper, ILogger<UpdateProjectResourceCommandHandler> logger)
        {
            _projectRepository = projectRepository;
            _projectResourceRepository = projectResourceRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateProjectResourceCommand request, CancellationToken cancellationToken)
        {
            var entityProject = await _projectRepository.GetByIdAsync(request.ProjectId);
            if (entityProject == null)
            {
                _logger.LogError($"Error: Project does not exist");
                throw new NotFoundException(nameof(Project), request.ProjectId);
            }

            var entityToUpdate = await _projectResourceRepository.GetByIdAsync(request.ProjectResourceId);
            if (entityToUpdate == null)
            {
                _logger.LogError($"Error: Project resource does not exist");
                throw new NotFoundException(nameof(ProjectResource), request.ProjectResourceId);
            }

            _mapper.Map(request, entityToUpdate, typeof(UpdateProjectResourceCommand), typeof(ProjectResource));

            await _projectResourceRepository.UpdateAsync(entityToUpdate);

            _logger.LogInformation($"Project resource {entityToUpdate.Id} successfully updated");

            return Unit.Value;
        }
    }
}
