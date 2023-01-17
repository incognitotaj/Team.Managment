using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Team.Application.Contracts.Persistence;
using Team.Application.Exceptions;
using Team.Domain.Entities;

namespace Team.Application.Features.Projects.Commands.UpdateProject
{
    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateProjectCommandHandler> _logger;

        public UpdateProjectCommandHandler(IProjectRepository projectRepository, IMapper mapper, ILogger<UpdateProjectCommandHandler> logger)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var entityToUpdate = await _projectRepository.GetByIdAsync(request.ProjectId);
            if (entityToUpdate == null)
            {
                _logger.LogError($"Error: Project does not exist");
                throw new NotFoundException(nameof(Project), request.ProjectId);
            }

            _mapper.Map(request, entityToUpdate, typeof(UpdateProjectCommand), typeof(Project));

            await _projectRepository.UpdateAsync(entityToUpdate);

            _logger.LogInformation($"Project {entityToUpdate.Id} successfully updated");

            return Unit.Value;
        }
    }
}
