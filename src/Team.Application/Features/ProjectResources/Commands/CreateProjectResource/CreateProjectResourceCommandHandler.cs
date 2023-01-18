using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Team.Application.Contracts.Persistence;
using Team.Application.Exceptions;
using Team.Domain.Entities;

namespace Team.Application.Features.ProjectResources.Commands.CreateProjectResource
{
    public class CreateProjectResourceCommandHandler : IRequestHandler<CreateProjectResourceCommand, Guid>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectResourceRepository _projectResourceRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateProjectResourceCommandHandler> _logger;

        public CreateProjectResourceCommandHandler(IProjectRepository projectRepository, IProjectResourceRepository projectResourceRepository, IMapper mapper, ILogger<CreateProjectResourceCommandHandler> logger)
        {
            _projectRepository = projectRepository;
            _projectResourceRepository = projectResourceRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Guid> Handle(CreateProjectResourceCommand request, CancellationToken cancellationToken)
        {
            var entityProject = await _projectRepository.GetByIdAsync(request.ProjectId);
            if (entityProject == null)
            {
                _logger.LogError($"Error: Project does not exist");
                throw new NotFoundException(nameof(Project), request.ProjectId);
            }

            var entity = _mapper.Map<ProjectResource>(request);

            var newEntity = await _projectResourceRepository.AddAsync(entity);

            _logger.LogInformation($"Project resource {newEntity.Id} created successfully on {newEntity.CreatedOn}");

            return newEntity.Id;
        }
    }
}
