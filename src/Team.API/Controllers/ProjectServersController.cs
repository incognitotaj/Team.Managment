using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Team.API.Requests;
using Team.Application.Dtos;
using Team.Application.Features.ProjectServers.Commands.CreateProjectServer;
using Team.Application.Features.ProjectServers.Commands.DeleteProjectServer;
using Team.Application.Features.ProjectServers.Commands.UpdateProjectServer;
using Team.Application.Features.ProjectServers.Queries.GetById;
using Team.Application.Features.ProjectServers.Queries.GetByProjectId;

namespace Team.API.Controllers
{
    [Route("api/projects/{projectId}/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Authorize]
    public class ProjectServersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectServersController(IMediator mediator)
        {
            _mediator = mediator;
        }


        /// <summary>
        /// Get specific project Server by ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("{projectServerId}")]
        [ProducesResponseType(typeof(ProjectServerDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ProjectServerDto>> Get(string projectId, string projectServerId)
        {
            var query = new GetByIdQuery()
            {
                ProjectId = Guid.Parse(projectId),
                ProjectServerId = Guid.Parse(projectServerId)
            };

            return Ok(await _mediator.Send(query));
        }

        /// <summary>
        /// Get list of all project Servers for the specific project
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<ProjectServerDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<ProjectServerDto>>> GetByProject(string projectId)
        {
            var query = new GetByProjectIdQuery
            {
                ProjectId = Guid.Parse(projectId)
            };

            return Ok(await _mediator.Send(query));
        }

        /// <summary>
        /// Creates a new project Server for the project
        /// </summary>
        /// <param name="projectId">Project Id</param>
        /// <param name="request">Project Server data</param>
        /// <returns></returns>
        [HttpPost()]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult<Guid>> Create(string projectId, [FromBody] CreateProjectServerRequest request)
        {
            var command = new CreateProjectServerCommand
            {
                ProjectId = Guid.Parse(projectId),
                Password = request.Password,
                Title = request.Title,
                Url = request.Url,
                Username = request.Username
            };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Update an existing project Server
        /// </summary>
        /// <param name="projectId">Project ID</param>
        /// <param name="request">Project Server data</param>
        /// <returns></returns>
        [HttpPut()]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> Update(string projectId, [FromBody] UpdateProjectServerRequest request)
        {
            var command = new UpdateProjectServerCommand
            {
                ProjectId = Guid.Parse(projectId),
                ProjectServerId = Guid.Parse(request.ProjectServerId),
                Password = request.Password,
                Title = request.Title,
                Url = request.Url,
                Username = request.Username
            };
            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Deletes an existing project Server
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="projectServerId"></param>
        /// <returns></returns>
        [HttpDelete("{projectServerId}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> Delete(string projectId, string projectServerId)
        {
            var command = new DeleteProjectServerCommand()
            {
                ProjectId = Guid.Parse(projectId),
                ProjectServerId = Guid.Parse(projectServerId)
            };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
