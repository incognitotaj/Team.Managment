using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Team.Application.Contracts.Persistence;
using Team.Application.Dtos;
using Team.Application.Exceptions;
using Team.Domain.Entities;

namespace Team.Application.Features.ProjectResources.Queries.GetById
{
    public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, ProjectResourceDto>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectResourceRepository _projectResourceRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetByIdQueryHandler> _logger;

        public GetByIdQueryHandler(IProjectRepository projectRepository, IProjectResourceRepository projectResourceRepository, IMapper mapper, ILogger<GetByIdQueryHandler> logger)
        {
            _projectRepository = projectRepository;
            _projectResourceRepository = projectResourceRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ProjectResourceDto> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var entityProject = await _projectRepository.GetByIdAsync(request.ProjectId);
            if (entityProject == null)
            {
                _logger.LogError($"Error: Project does not exist");
                throw new NotFoundException(nameof(Project), request.ProjectId);
            }

            var entity = await _projectResourceRepository.GetByIdAsync(request.ProjectResourceId);
            if (entity == null)
            {
                _logger.LogError($"Error: Project resource does not exist");
                throw new NotFoundException(nameof(ProjectResource), request.ProjectResourceId);
            }

            return _mapper.Map<ProjectResourceDto>(entity);
        }
    }
}
