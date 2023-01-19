using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Team.Application.Contracts.Persistence;
using Team.Application.Dtos;
using Team.Application.Exceptions;
using Team.Domain.Entities;

namespace Team.Application.Features.ProjectServers.Queries.GetById
{
    public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, ProjectServerDto>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectServerRepository _projectServerRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetByIdQueryHandler> _logger;

        public GetByIdQueryHandler(IProjectRepository projectRepository, IProjectServerRepository projectServerRepository, IMapper mapper, ILogger<GetByIdQueryHandler> logger)
        {
            _projectRepository = projectRepository;
            _projectServerRepository = projectServerRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ProjectServerDto> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var entityProject = await _projectRepository.GetByIdAsync(request.ProjectId);
            if (entityProject == null)
            {
                _logger.LogError($"Error: Project does not exist");
                throw new NotFoundException(nameof(Project), request.ProjectId);
            }

            var entity = await _projectServerRepository.GetByIdAsync(request.ProjectServerId);
            if (entity == null)
            {
                _logger.LogError($"Error: Project Server does not exist");
                throw new NotFoundException(nameof(ProjectServer), request.ProjectServerId);
            }

            return _mapper.Map<ProjectServerDto>(entity);
        }
    }
}
