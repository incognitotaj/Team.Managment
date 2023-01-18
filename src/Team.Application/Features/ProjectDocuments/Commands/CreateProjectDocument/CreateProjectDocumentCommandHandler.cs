using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Team.Application.Contracts.Persistence;
using Team.Application.Exceptions;
using Team.Domain.Entities;

namespace Team.Application.Features.ProjectDocuments.Commands.CreateProjectDocument
{
    public class CreateProjectDocumentCommandHandler : IRequestHandler<CreateProjectDocumentCommand, Guid>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectDocumentRepository _projectDocumentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateProjectDocumentCommandHandler> _logger;

        public CreateProjectDocumentCommandHandler(IProjectRepository projectRepository, IProjectDocumentRepository projectDocumentRepository, IMapper mapper, ILogger<CreateProjectDocumentCommandHandler> logger)
        {
            _projectRepository = projectRepository;
            _projectDocumentRepository = projectDocumentRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Guid> Handle(CreateProjectDocumentCommand request, CancellationToken cancellationToken)
        {
            var entityProject = await _projectRepository.GetByIdAsync(request.ProjectId);
            if (entityProject == null)
            {
                _logger.LogError($"Error: Project does not exist");
                throw new NotFoundException(nameof(Project), request.ProjectId);
            }

            var entity = _mapper.Map<ProjectDocument>(request);

            var newEntity = await _projectDocumentRepository.AddAsync(entity);

            _logger.LogInformation($"Project document {newEntity.Id} created successfully on {newEntity.CreatedOn}");

            return newEntity.Id;
        }
    }
}
