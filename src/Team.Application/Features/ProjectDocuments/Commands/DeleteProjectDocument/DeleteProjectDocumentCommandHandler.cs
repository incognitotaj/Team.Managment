using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Team.Application.Contracts.Persistence;
using Team.Application.Exceptions;
using Team.Domain.Entities;

namespace Team.Application.Features.ProjectDocuments.Commands.DeleteProjectDocument
{
    public class DeleteProjectDocumentCommandHandler : IRequestHandler<DeleteProjectDocumentCommand>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectDocumentRepository _projectDocumentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteProjectDocumentCommandHandler> _logger;

        public DeleteProjectDocumentCommandHandler(IProjectRepository projectRepository, IProjectDocumentRepository projectDocumentRepository, IMapper mapper, ILogger<DeleteProjectDocumentCommandHandler> logger)
        {
            _projectRepository = projectRepository;
            _projectDocumentRepository = projectDocumentRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteProjectDocumentCommand request, CancellationToken cancellationToken)
        {
            var entityProject = await _projectRepository.GetByIdAsync(request.ProjectId);
            if (entityProject == null)
            {
                _logger.LogError($"Error: Project does not exist");
                throw new NotFoundException(nameof(Project), request.ProjectId);
            }

            var entityToDelete = await _projectDocumentRepository.GetByIdAsync(request.ProjectDocumentId);
            if (entityToDelete == null)
            {
                _logger.LogError($"Error: Project document does not exist");
                throw new NotFoundException(nameof(ProjectDocument), request.ProjectDocumentId);
            }

            await _projectDocumentRepository.DeleteAsync(entityToDelete);
            _logger.LogInformation($"Project document {entityToDelete.Id} successfully deleted");

            return Unit.Value;
        }
    }
}
