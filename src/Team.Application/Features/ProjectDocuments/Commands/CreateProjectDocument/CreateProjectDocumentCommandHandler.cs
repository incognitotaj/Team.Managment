using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Team.Application.Contracts.Persistence;
using Team.Application.Contracts.Services;
using Team.Application.Exceptions;
using Team.Domain.Entities;

namespace Team.Application.Features.ProjectDocuments.Commands.CreateProjectDocument
{
    public class CreateProjectDocumentCommandHandler : IRequestHandler<CreateProjectDocumentCommand, Guid>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectDocumentRepository _projectDocumentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateProjectDocumentCommandHandler> _logger;
        private readonly IFileUploadOnServerService _fileUploadOnServerService;

        public CreateProjectDocumentCommandHandler(
            IProjectRepository projectRepository, 
            IProjectDocumentRepository projectDocumentRepository, 
            IMapper mapper, 
            ILogger<CreateProjectDocumentCommandHandler> logger,
            IFileUploadOnServerService fileUploadOnServerService)
        {
            _projectRepository = projectRepository;
            _projectDocumentRepository = projectDocumentRepository;
            _mapper = mapper;
            _logger = logger;
            _fileUploadOnServerService = fileUploadOnServerService;
        }

        public async Task<Guid> Handle(CreateProjectDocumentCommand request, CancellationToken cancellationToken)
        {
            var entityProject = await _projectRepository.GetByIdAsync(request.ProjectId);
            if (entityProject == null)
            {
                _logger.LogError($"Error: Project does not exist");
                throw new NotFoundException(nameof(Project), request.ProjectId);
            }

            FileInfo fi = new FileInfo(request.Document.FileName);

            var projectName = entityProject.Id.ToString();
            var basePath = "documents";
            var fileName = DateTime.Now.Ticks.ToString();
            var url = $"/{basePath}/{projectName}/{fileName}{fi.Extension}";

            if (await _fileUploadOnServerService.UploadFile(
                file: request.Document,
                baseUrl: basePath, 
                directoryName: projectName, 
                fileName: fileName, 
                fileExtension: fi.Extension))
            {
                var entity = new ProjectDocument
                (
                    title: request.Title,
                    filePath: url,
                    detail: request.Detail,
                    projectId: request.ProjectId
                );

                var newEntity = await _projectDocumentRepository.AddAsync(entity);

                _logger.LogInformation($"Project document {newEntity.Id} created successfully on {newEntity.CreatedOn}");

                return newEntity.Id;
            }

            throw new Exception("Error while saving file on server");
        }
    }
}
