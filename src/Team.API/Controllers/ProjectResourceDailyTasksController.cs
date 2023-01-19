using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Team.API.Requests;
using Team.Application.Dtos;
using Team.Application.Features.PrjectResourceDailyTasks.Commands.DeleteProjectResourceDailyTask;
using Team.Application.Features.PrjectResourceDailyTasks.Commands.UpdateProjectResourceDailyTask;
using Team.Application.Features.ProjectResourceDailyTasks.Commands.CreateProjectResourceDailyTask;
using Team.Application.Features.ProjectResourceDailyTasks.Queries.GetById;
using Team.Application.Features.ProjectResourceDailyTasks.Queries.GetByProjectResourceId;

namespace Team.API.Controllers
{
    [Route("api/projects/{projectResourceId}/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Authorize]
    public class ProjectResourceDailyTasksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectResourceDailyTasksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get specific project resource daily task by ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("{projectResourceDailyTaskId}")]
        [ProducesResponseType(typeof(ProjectResourceDailyTaskDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ProjectResourceDailyTaskDto>> Get(string projectResourceId, string projectResourceDailyTaskId)
        {
            var query = new GetByIdQuery()
            {
                ProjectResourceId = Guid.Parse(projectResourceId),
                ProjectResourceDailyTaskId = Guid.Parse(projectResourceDailyTaskId)
            };

            return Ok(await _mediator.Send(query));
        }

        /// <summary>
        /// Get list of all daily tasks for the specific project resource
        /// </summary>
        /// <param name="projectResourceId"></param>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<ProjectResourceDailyTaskDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<ProjectResourceDailyTaskDto>>> GetByUser(string projectResourceId)
        {
            var query = new GetByProjectResourceIdQuery
            {
                ProjectResourceId = Guid.Parse(projectResourceId)
            };

            return Ok(await _mediator.Send(query));
        }

        /// <summary>
        /// Registers a new daily task for project resource
        /// </summary>
        /// <param name="projectResourceId">Project resource id</param>
        /// <param name="request">Project resource data</param>
        /// <returns></returns>
        [HttpPost()]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult<Guid>> Create(string projectResourceId, [FromBody] CreateProjectResourceDailyTaskRequest request)
        {
            var command = new CreateProjectResourceDailyTaskCommand
            {
                ProjectResourceId = Guid.Parse(projectResourceId),
                Description = request.Description,
                TaskStatus = request.TaskStatus,
                Title = request.Title
            };
            var result = await _mediator.Send(command);
            return Ok(result);
        }


        /// <summary>
        /// Update an existing daily task for the project resource
        /// </summary>
        /// <param name="projectResourceId">Project ID</param>
        /// <param name="request">Project resource daily task data</param>
        /// <returns></returns>
        [HttpPut()]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> Update(string projectResourceId, [FromBody] UpdateProjectResourceDailyTaskRequest request)
        {
            var command = new UpdateProjectResourceDailyTaskCommand
            {
                ProjectResourceDailyTaskId = Guid.Parse(request.ProjectResourceDailyTaskId),
                ProjectResourceId = Guid.Parse(projectResourceId),
                Description = request.Description,
                TaskStatus = request.TaskStatus,
                Title = request.Title
            };
            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Deletes / Removes an existing daily task for project resource
        /// </summary>
        /// <param name="projectResourceId"></param>
        /// <param name="projectResourceDailyTaskId"></param>
        /// <returns></returns>
        [HttpDelete("{projectResourceId}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> Delete(string projectResourceId, string projectResourceDailyTaskId)
        {
            var command = new DeleteProjectResourceDailyTaskCommand()
            {
                ProjectResourceId = Guid.Parse(projectResourceId),
                ProjectResourceDailyTaskId = Guid.Parse(projectResourceDailyTaskId)
            };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
