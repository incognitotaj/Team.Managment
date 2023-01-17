using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Team.Application.Contracts.Persistence;
using Team.Application.Dtos;
using Team.Application.Exceptions;
using Team.Domain.Entities;

namespace Team.Application.Features.ProjectClients.Queries.GetById
{
    public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, ProjectClientDto>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectClientRepository _projectClientRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetByIdQueryHandler> _logger;

        public GetByIdQueryHandler(IProjectRepository projectRepository, IProjectClientRepository projectClientRepository, IMapper mapper, ILogger<GetByIdQueryHandler> logger)
        {
            _projectRepository = projectRepository;
            _projectClientRepository = projectClientRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ProjectClientDto> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var entityProject = await _projectRepository.GetByIdAsync(request.ProjectId);
            if (entityProject == null)
            {
                _logger.LogError($"Error: Project does not exist");
                throw new NotFoundException(nameof(Project), request.ProjectId);
            }

            var entity = await _projectClientRepository.GetByIdAsync(request.ProjectClientId);
            if (entity == null)
            {
                _logger.LogError($"Error: Project client does not exist");
                throw new NotFoundException(nameof(ProjectClient), request.ProjectClientId);
            }

            return _mapper.Map<ProjectClientDto>(entity);
        }
    }
}
