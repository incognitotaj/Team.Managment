using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Team.Application.Contracts.Persistence;
using Team.Application.Exceptions;
using Team.Application.Features.Projects.Commands.CreateProject;
using Team.Domain.Entities;

namespace Team.Application.Features.ProjectClients.Commands.CreateProjectClient
{
    public class CreateProjectClientCommandHandler : IRequestHandler<CreateProjectClientCommand, Guid>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectClientRepository _projectClientRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateProjectCommandHandler> _logger;

        public CreateProjectClientCommandHandler(IProjectRepository projectRepository, IProjectClientRepository projectClientRepository, IMapper mapper, ILogger<CreateProjectCommandHandler> logger)
        {
            _projectRepository = projectRepository;
            _projectClientRepository = projectClientRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Guid> Handle(CreateProjectClientCommand request, CancellationToken cancellationToken)
        {
            var entityProject = await _projectRepository.GetByIdAsync(request.ProjectId);
            if (entityProject == null)
            {
                _logger.LogError($"Error: Project does not exist");
                throw new NotFoundException(nameof(Project), request.ProjectId);
            }

            var entity = _mapper.Map<ProjectClient>(request);

            var newEntity = await _projectClientRepository.AddAsync(entity);

            _logger.LogInformation($"Project client {newEntity.Id} created successfully on {newEntity.CreatedOn}");

            return newEntity.Id;
        }
    }
}
