using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Team.API.Requests;
using Team.Application.Dtos;
using Team.Application.Features.ProjectResources.Commands.CreateProjectResource;
using Team.Application.Features.ProjectResources.Commands.DeleteProjectResource;
using Team.Application.Features.ProjectResources.Commands.UpdateProjectResource;
using Team.Application.Features.ProjectResources.Queries.GetById;
using Team.Application.Features.ProjectResources.Queries.GetByProjectId;

namespace Team.API.Controllers
{
    [Route("api/projects/{projectId}/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Authorize]
    public class ProjectResourcesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectResourcesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get specific project resource by ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("{projectResourceId}")]
        [ProducesResponseType(typeof(ProjectResourceDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ProjectResourceDto>> Get(string projectId, string projectResourceId)
        {
            var query = new GetByIdQuery()
            {
                ProjectId = Guid.Parse(projectId),
                ProjectResourceId = Guid.Parse(projectResourceId)
            };

            return Ok(await _mediator.Send(query));
        }

        /// <summary>
        /// Get list of all project resources for the specific project
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<ProjectResourceDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<ProjectResourceDto>>> GetByProject(string projectId)
        {
            var query = new GetByProjectIdQuery
            {
                ProjectId = Guid.Parse(projectId)
            };

            return Ok(await _mediator.Send(query));
        }

        /// <summary>
        /// Registers / Assigns a new resource to the project
        /// </summary>
        /// <param name="projectId">Project Id</param>
        /// <param name="request">Project resource data</param>
        /// <returns></returns>
        [HttpPost()]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult<Guid>> Create(string projectId, [FromBody] CreateProjectResourceRequest request)
        {
            var command = new CreateProjectResourceCommand
            {
                ProjectId = Guid.Parse(projectId),
                FromDate = request.FromDate,
                ToDate = request.ToDate,
                ResourceId = Guid.Parse(request.ResourceId)
            };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Update an existing project resource
        /// </summary>
        /// <param name="projectId">Project ID</param>
        /// <param name="request">Project resource data</param>
        /// <returns></returns>
        [HttpPut()]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> Update(string projectId, [FromBody] UpdateProjectResourceRequest request)
        {
            var command = new UpdateProjectResourceCommand
            {
                ProjectId = Guid.Parse(projectId),
                ProjectResourceId = Guid.Parse(request.ProjectResourceId),
                FromDate = request.FromDate,
                ToDate = request.ToDate,
                ResourceId = Guid.Parse(request.ResourceId)
            };
            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Deletes / Removes an existing resource from project
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="projectResourceId"></param>
        /// <returns></returns>
        [HttpDelete("{projectResourceId}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> Delete(string projectId, string projectResourceId)
        {
            var command = new DeleteProjectResourceCommand()
            {
                ProjectId = Guid.Parse(projectId),
                ProjectResourceId = Guid.Parse(projectResourceId)
            };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
