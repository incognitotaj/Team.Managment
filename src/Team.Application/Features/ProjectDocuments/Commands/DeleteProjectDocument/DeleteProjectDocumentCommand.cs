using MediatR;

namespace Team.Application.Features.ProjectDocuments.Commands.DeleteProjectDocument
{
    public class DeleteProjectDocumentCommand : IRequest
    {
        public Guid ProjectDocumentId { get; set; }
        public Guid ProjectId { get; set; }
    }
}
