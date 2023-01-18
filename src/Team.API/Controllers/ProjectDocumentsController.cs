using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Team.API.Requests;
using Team.Application.Dtos;
using Team.Application.Features.ProjectDocuments.Commands.CreateProjectDocument;
using Team.Application.Features.ProjectDocuments.Commands.DeleteProjectDocument;
using Team.Application.Features.ProjectDocuments.Commands.UpdateProjectDocument;
using Team.Application.Features.ProjectDocuments.Queries.GetById;
using Team.Application.Features.ProjectDocuments.Queries.GetByProjectId;

namespace Team.API.Controllers
{
    [Route("api/projects/{projectId}/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ProjectDocumentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectDocumentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get specific project document by ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("{projectDocumentId}")]
        [ProducesResponseType(typeof(ProjectDocumentDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ProjectDocumentDto>> Get(string projectId, string projectDocumentId)
        {
            var query = new GetByIdQuery()
            {
                ProjectId = Guid.Parse(projectId),
                ProjectDocumentId = Guid.Parse(projectDocumentId)
            };

            return Ok(await _mediator.Send(query));
        }

        /// <summary>
        /// Get list of all project documents for the specific project
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<ProjectDocumentDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<ProjectDocumentDto>>> GetByUser(string projectId)
        {
            var query = new GetByProjectIdQuery
            {
                ProjectId = Guid.Parse(projectId)
            };

            return Ok(await _mediator.Send(query));
        }

        /// <summary>
        /// Creates a new project client for the project
        /// </summary>
        /// <param name="projectId">Project Id</param>
        /// <param name="request">Project client data</param>
        /// <returns></returns>
        [HttpPost()]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult<Guid>> Create(string projectId, [FromBody] CreateProjectDocumentRequest request)
        {
            var command = new CreateProjectDocumentCommand
            {
                ProjectId = Guid.Parse(projectId),
                Detail = request.Detail,
                Document = request.Document,
                Title = request.Title
            };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Update an existing project Document
        /// </summary>
        /// <param name="projectId">Project ID</param>
        /// <param name="request">Project Document data</param>
        /// <returns></returns>
        [HttpPut()]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> Update(string projectId, [FromBody] UpdateProjectDocumentRequest request)
        {
            var command = new UpdateProjectDocumentCommand
            {
                ProjectId = Guid.Parse(projectId),
                ProjectDocumentId = Guid.Parse(request.ProjectDocumentId),
                Detail = request.Detail,
                Document = request.Document,
                Title = request.Title
            };
            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Deletes an existing project Document
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="projectDocumentId"></param>
        /// <returns></returns>
        [HttpDelete("{projectDocumentId}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> Delete(string projectId, string projectDocumentId)
        {
            var command = new DeleteProjectDocumentCommand()
            {
                ProjectId = Guid.Parse(projectId),
                ProjectDocumentId = Guid.Parse(projectDocumentId)
            };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
