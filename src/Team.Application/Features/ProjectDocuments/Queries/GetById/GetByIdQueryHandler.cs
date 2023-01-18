using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Team.Application.Contracts.Persistence;
using Team.Application.Dtos;
using Team.Application.Exceptions;
using Team.Domain.Entities;

namespace Team.Application.Features.ProjectDocuments.Queries.GetById
{
    public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, ProjectDocumentDto>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectDocumentRepository _projectDocumentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetByIdQueryHandler> _logger;

        public GetByIdQueryHandler(IProjectRepository projectRepository, IProjectDocumentRepository projectDocumentRepository, IMapper mapper, ILogger<GetByIdQueryHandler> logger)
        {
            _projectRepository = projectRepository;
            _projectDocumentRepository = projectDocumentRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ProjectDocumentDto> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var entityProject = await _projectRepository.GetByIdAsync(request.ProjectId);
            if (entityProject == null)
            {
                _logger.LogError($"Error: Project does not exist");
                throw new NotFoundException(nameof(Project), request.ProjectId);
            }

            var entity = await _projectDocumentRepository.GetByIdAsync(request.ProjectDocumentId);
            if (entity == null)
            {
                _logger.LogError($"Error: Project document does not exist");
                throw new NotFoundException(nameof(ProjectDocument), request.ProjectDocumentId);
            }

            return _mapper.Map<ProjectDocumentDto>(entity);
        }
    }
}
