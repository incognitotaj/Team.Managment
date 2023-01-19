using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Team.Application.Contracts.Persistence;
using Team.Application.Contracts.Services;
using Team.Application.Exceptions;
using Team.Domain.Entities;

namespace Team.Application.Features.ProjectDocuments.Commands.UpdateProjectDocument
{
    public class UpdateProjectDocumentCommandHandler : IRequestHandler<UpdateProjectDocumentCommand>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectDocumentRepository _projectDocumentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateProjectDocumentCommandHandler> _logger;
        private readonly IFileUploadOnServerService _fileUploadOnServerService;

        public UpdateProjectDocumentCommandHandler(
            IProjectRepository projectRepository, 
            IProjectDocumentRepository projectDocumentRepository, 
            IMapper mapper, 
            ILogger<UpdateProjectDocumentCommandHandler> logger,
            IFileUploadOnServerService fileUploadOnServerService)
        {
            _projectRepository = projectRepository;
            _projectDocumentRepository = projectDocumentRepository;
            _mapper = mapper;
            _logger = logger;
            _fileUploadOnServerService = fileUploadOnServerService;
        }

        public async Task<Unit> Handle(UpdateProjectDocumentCommand request, CancellationToken cancellationToken)
        {
            var entityProject = await _projectRepository.GetByIdAsync(request.ProjectId);
            if (entityProject == null)
            {
                _logger.LogError($"Error: Project does not exist");
                throw new NotFoundException(nameof(Project), request.ProjectId);
            }

            var entityToUpdate = await _projectDocumentRepository.GetByIdAsync(request.ProjectDocumentId);
            if (entityToUpdate == null)
            {
                _logger.LogError($"Error: Project document does not exist");
                throw new NotFoundException(nameof(ProjectDocument), request.ProjectDocumentId);
            }

            _mapper.Map(request, entityToUpdate, typeof(UpdateProjectDocumentCommand), typeof(ProjectDocument));

            if (request.Document != null)
            {
                FileInfo fi = new FileInfo(request.Document.FileName);

                var projectName = entityProject.Id.ToString();
                var basePath = "documents";
                var fileName = DateTime.Now.Ticks.ToString();
                var url = $"/{basePath}/{projectName}/{fileName}{fi.Extension}";

                await _fileUploadOnServerService.UploadFile(
                    file: request.Document,
                    baseUrl: basePath,
                    directoryName: projectName,
                    fileName: fileName,
                    fileExtension: fi.Extension);

                entityToUpdate.FilePath = url;
            }

            await _projectDocumentRepository.UpdateAsync(entityToUpdate);

            _logger.LogInformation($"Project document {entityToUpdate.Id} successfully updated");

            return Unit.Value;
        }
    }
}
