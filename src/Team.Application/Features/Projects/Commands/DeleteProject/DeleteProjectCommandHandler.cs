using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Team.Application.Contracts.Persistence;
using Team.Application.Exceptions;
using Team.Domain.Entities;

namespace Team.Application.Features.Projects.Commands.DeleteProject
{
    public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteProjectCommandHandler> _logger;

        public DeleteProjectCommandHandler(IProjectRepository projectRepository, IMapper mapper, ILogger<DeleteProjectCommandHandler> logger)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var entityToDelete = await _projectRepository.GetByIdAsync(request.ProjectId);
            if (entityToDelete == null)
            {
                _logger.LogError($"Error: Project does not exist");
                throw new NotFoundException(nameof(Project), request.ProjectId);
            }

            await _projectRepository.DeleteAsync(entityToDelete);
            _logger.LogInformation($"Project {entityToDelete.Id} successfully deleted");

            return Unit.Value;
        }
    }
}
