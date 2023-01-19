using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Team.Application.Contracts.Persistence;
using Team.Application.Exceptions;
using Team.Domain.Entities;

namespace Team.Application.Features.ProjectServers.Commands.CreateProjectServer
{
    public class CreateProjectServerCommandHandler : IRequestHandler<CreateProjectServerCommand, Guid>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectServerRepository _projectServerRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateProjectServerCommandHandler> _logger;

        public CreateProjectServerCommandHandler(IProjectRepository projectRepository, IProjectServerRepository projectServerRepository, IMapper mapper, ILogger<CreateProjectServerCommandHandler> logger)
        {
            _projectRepository = projectRepository;
            _projectServerRepository = projectServerRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Guid> Handle(CreateProjectServerCommand request, CancellationToken cancellationToken)
        {
            var entityProject = await _projectRepository.GetByIdAsync(request.ProjectId);
            if (entityProject == null)
            {
                _logger.LogError($"Error: Project does not exist");
                throw new NotFoundException(nameof(Project), request.ProjectId);
            }

            var entity = _mapper.Map<ProjectServer>(request);

            var newEntity = await _projectServerRepository.AddAsync(entity);

            _logger.LogInformation($"Project Server {newEntity.Id} created successfully on {newEntity.CreatedOn}");

            return newEntity.Id;
        }
    }
}
