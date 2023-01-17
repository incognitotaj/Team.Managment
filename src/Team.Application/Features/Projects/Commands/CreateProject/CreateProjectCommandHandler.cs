using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team.Application.Contracts.Persistence;
using Team.Domain.Entities;

namespace Team.Application.Features.Projects.Commands.CreateProject
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Guid>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateProjectCommandHandler> _logger;

        public CreateProjectCommandHandler(IProjectRepository projectRepository, IMapper mapper, ILogger<CreateProjectCommandHandler> logger)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Guid> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Project>(request);

            var newEntity =  await _projectRepository.AddAsync(entity);

            _logger.LogInformation($"Project {newEntity.Id} created successfully on {newEntity.CreatedOn}");

            return newEntity.Id;
        }
    }
}
