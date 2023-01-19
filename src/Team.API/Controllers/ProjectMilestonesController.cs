using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Team.API.Requests;
using Team.Application.Dtos;
using Team.Application.Features.ProjectMilestones.Commands.CreateProjectMilestone;
using Team.Application.Features.ProjectMilestones.Commands.DeleteProjectMilestone;
using Team.Application.Features.ProjectMilestones.Commands.UpdateProjectMilestone;
using Team.Application.Features.ProjectMilestones.Queries.GetById;
using Team.Application.Features.ProjectMilestones.Queries.GetByProjectId;

namespace Team.API.Controllers
{
    [Route("api/projects/{projectId}/[controller]")]
    [ApiController]
    [Produces("application/json")]
    //[Authorize]
    public class ProjectMilestonesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectMilestonesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get specific project milestone by ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("{projectMilestoneId}")]
        [ProducesResponseType(typeof(ProjectMilestoneDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ProjectMilestoneDto>> Get(string projectId, string projectMilestoneId)
        {
            var query = new GetByIdQuery()
            {
                ProjectId = Guid.Parse(projectId),
                ProjectMilestoneId = Guid.Parse(projectMilestoneId)
            };

            return Ok(await _mediator.Send(query));
        }

        /// <summary>
        /// Get list of all project milestones for the specific project
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<ProjectMilestoneDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<ProjectMilestoneDto>>> GetByUser(string projectId)
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
        /// <param name="request">Project milestone data</param>
        /// <returns></returns>
        [HttpPost()]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult<Guid>> Create(string projectId, [FromBody] CreateProjectMilestoneRequest request)
        {
            var command = new CreateProjectMilestoneCommand
            {
                ProjectId = Guid.Parse(projectId),
                Detail = request.Detail,
                FromDate = request.FromDate,
                Title = request.Title,
                ToDate = request.ToDate
            };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Update an existing project milestone
        /// </summary>
        /// <param name="projectId">Project ID</param>
        /// <param name="request">Project client data</param>
        /// <returns></returns>
        [HttpPut()]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> Update(string projectId, [FromBody] UpdateProjectMilestoneRequest request)
        {
            var command = new UpdateProjectMilestoneCommand
            {
                ProjectId = Guid.Parse(projectId),
                ProjectMilestoneId = Guid.Parse(request.ProjectMilestoneId),
                Detail = request.Detail,
                FromDate = request.FromDate,
                Title = request.Title,
                ToDate = request.ToDate
            };
            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Deletes an existing project milestone
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="projectMilestoneId"></param>
        /// <returns></returns>
        [HttpDelete("{projectMilestoneId}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> Delete(string projectId, string projectMilestoneId)
        {
            var command = new DeleteProjectMilestoneCommand()
            {
                ProjectId = Guid.Parse(projectId),
                ProjectMilestoneId = Guid.Parse(projectMilestoneId)
            };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
