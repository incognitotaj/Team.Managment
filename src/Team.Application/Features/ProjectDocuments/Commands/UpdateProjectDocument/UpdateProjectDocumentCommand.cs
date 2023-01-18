using MediatR;
using Microsoft.AspNetCore.Http;

namespace Team.Application.Features.ProjectDocuments.Commands.UpdateProjectDocument
{
    public class UpdateProjectDocumentCommand : IRequest
    {
        public Guid ProjectDocumentId { get; set; }
        public Guid ProjectId { get; set; }
        public string Title { get; set; }
        public IFormFile Document { get; set; }
        public string Detail { get; set; }
    }
}
