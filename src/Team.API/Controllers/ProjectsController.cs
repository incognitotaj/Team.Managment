using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Team.Application.Dtos;
using Team.Application.Features.Projects.Commands.CreateProject;
using Team.Application.Features.Projects.Commands.DeleteProject;
using Team.Application.Features.Projects.Commands.UpdateProject;
using Team.Application.Features.Projects.Queries.GetProjectById;
using Team.Application.Features.Projects.Queries.GetProjectsList;
using Team.Application.Features.Projects.Queries.GetProjectsListByUser;

namespace Team.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get list of all projects
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<ProjectDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> Get()
        {
            var query = new GetProjectsListQuery();

            return Ok(await _mediator.Send(query));
        }

        /// <summary>
        /// Get list of all projects for the specific user as manager
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("get-by-user/{userId}")]
        [ProducesResponseType(typeof(IEnumerable<ProjectDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> GetByUser(string userId)
        {
            var query = new GetProjectsListByUserQuery(Guid.Parse(userId));

            return Ok(await _mediator.Send(query));
        }

        /// <summary>
        /// Get a single specific project by it's unique id (GUID)
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet("{projectId}")]
        [ProducesResponseType(typeof(ProjectDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ProjectDto>> GetById(string projectId)
        {
            var query = new GetProjectByIdQuery()
            {
                ProjectId = Guid.Parse(projectId)
            };

            return Ok(await _mediator.Send(query));
        }

        /// <summary>
        /// Creates / registers a new project
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost()]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> Create([FromBody] CreateProjectCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Updates an existing project
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut()]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> Update([FromBody] UpdateProjectCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Deletes an existing project
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpDelete("{projectId}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> Delete(string projectId)
        {
            var command = new DeleteProjectCommand()
            {
                ProjectId = Guid.Parse(projectId)
            };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
