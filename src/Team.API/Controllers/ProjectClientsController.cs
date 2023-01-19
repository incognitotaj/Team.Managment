using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Team.API.Requests;
using Team.Application.Dtos;
using Team.Application.Features.ProjectClients.Commands.CreateProjectClient;
using Team.Application.Features.ProjectClients.Commands.DeleteProjectClient;
using Team.Application.Features.ProjectClients.Commands.UpdateProjectClient;
using Team.Application.Features.ProjectClients.Queries.GetById;
using Team.Application.Features.ProjectClients.Queries.GetByProjectId;

namespace Team.API.Controllers
{
    [Route("api/projects/{projectId}/[controller]")]
    [ApiController]
    [Produces("application/json")]
    //[Authorize]
    public class ProjectClientsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectClientsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get specific project client by ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("{projectClientId}")]
        [ProducesResponseType(typeof(ProjectClientDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ProjectClientDto>> Get(string projectId, string projectClientId)
        {
            var query = new GetByIdQuery()
            {
                ProjectId = Guid.Parse(projectId),
                ProjectClientId = Guid.Parse(projectClientId)
            };

            return Ok(await _mediator.Send(query));
        }

        /// <summary>
        /// Get list of all project clients for the specific project
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<ProjectClientDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<ProjectClientDto>>> GetByUser(string projectId)
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
        public async Task<ActionResult<Guid>> Create(string projectId, [FromBody] CreateProjectClientRequest request)
        {
            var command = new CreateProjectClientCommand
            {
                ProjectId = Guid.Parse(projectId),
                Email = request.Email,
                Name = request.Name,
                Phone = request.Phone
            };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Update an existing project client
        /// </summary>
        /// <param name="projectId">Project ID</param>
        /// <param name="request">Project client data</param>
        /// <returns></returns>
        [HttpPut()]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> Update(string projectId, [FromBody] UpdateProjectClientRequest request)
        {
            var command = new UpdateProjectClientCommand
            {
                ProjectId = Guid.Parse(projectId),
                ProjectClientId = Guid.Parse(request.ProjectClientId),
                Email = request.Email,
                Name = request.Name,
                Phone = request.Phone
            };
            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Deletes an existing project client
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="projectClientId"></param>
        /// <returns></returns>
        [HttpDelete("{projectClientId}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> Delete(string projectId, string projectClientId)
        {
            var command = new DeleteProjectClientCommand()
            {
                ProjectId = Guid.Parse(projectId),
                ProjectClientId = Guid.Parse(projectClientId)
            };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
