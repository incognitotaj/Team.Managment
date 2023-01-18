using MediatR;
using Team.Application.Dtos;

namespace Team.Application.Features.ProjectDocuments.Queries.GetById
{
    public class GetByIdQuery : IRequest<ProjectDocumentDto>
    {
        public Guid ProjectId { get; set; }
        public Guid ProjectDocumentId { get; set; }
    }
}
