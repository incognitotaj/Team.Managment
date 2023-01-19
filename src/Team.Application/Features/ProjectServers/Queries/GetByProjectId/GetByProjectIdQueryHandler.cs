using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Team.Application.Contracts.Persistence;
using Team.Application.Dtos;
using Team.Application.Exceptions;
using Team.Domain.Entities;

namespace Team.Application.Features.ProjectServers.Queries.GetByProjectId
{
    public class GetByProjectIdQueryHandler : IRequestHandler<GetByProjectIdQuery, IEnumerable<ProjectServerDto>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectServerRepository _projectServerRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetByProjectIdQueryHandler> _logger;

        public GetByProjectIdQueryHandler(IProjectRepository projectRepository, IProjectServerRepository projectServerRepository, IMapper mapper, ILogger<GetByProjectIdQueryHandler> logger)
        {
            _projectRepository = projectRepository;
            _projectServerRepository = projectServerRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<ProjectServerDto>> Handle(GetByProjectIdQuery request, CancellationToken cancellationToken)
        {
            var entityProject = await _projectRepository.GetByIdAsync(request.ProjectId);
            if (entityProject == null)
            {
                _logger.LogError($"Error: Project does not exist");
                throw new NotFoundException(nameof(Project), request.ProjectId);
            }

            var projectServers = await _projectServerRepository.GetAsync(p => p.ProjectId == request.ProjectId);

            return _mapper.Map<IEnumerable<ProjectServerDto>>(projectServers);
        }
    }
}
