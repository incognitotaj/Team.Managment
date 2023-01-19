using MediatR;
using Microsoft.AspNetCore.Http;

namespace Team.Application.Features.ProjectDocuments.Commands.CreateProjectDocument
{
    public class CreateProjectDocumentCommand : IRequest<Guid>
    {
        public Guid ProjectId { get; set; }
        public string Title { get; set; }
        public IFormFile Document { get; set; }
        public string Detail { get; set; }
    }
}
