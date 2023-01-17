using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Team.Application.Contracts.Persistence;
using Team.Application.Dtos;
using Team.Application.Exceptions;
using Team.Domain.Entities;

namespace Team.Application.Features.Projects.Queries.GetProjectById
{
    public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ProjectDto>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetProjectByIdQueryHandler> _logger;

        public GetProjectByIdQueryHandler(IProjectRepository projectRepository, IMapper mapper, ILogger<GetProjectByIdQueryHandler> logger)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ProjectDto> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _projectRepository.GetByIdAsync(request.ProjectId);
            if (entity == null)
            {
                _logger.LogError($"Error: Project does not exist");
                throw new NotFoundException(nameof(Project), request.ProjectId);
            }

            return _mapper.Map<ProjectDto>(entity);
        }
    }
}
