using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Team.Application.Dtos;
using Team.Application.Features.Resources.Commands.CreateResource;
using Team.Application.Features.Resources.Commands.DeleteResource;
using Team.Application.Features.Resources.Commands.UpdateResource;
using Team.Application.Features.Resources.Queries.GetResourceById;
using Team.Application.Features.Resources.Queries.GetResourcesList;

namespace Team.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ResourcesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ResourcesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get list of all resource
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<ResourceDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ResourceDto>>> Get()
        {
            var query = new GetResourcesListQuery();

            return Ok(await _mediator.Send(query));
        }

        /// <summary>
        /// Get a single specific Resource by it's unique id (GUID)
        /// </summary>
        /// <param name="resourceId"></param>
        /// <returns></returns>
        [HttpGet("{resourceId}")]
        [ProducesResponseType(typeof(ProjectDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ResourceDto>> GetById(string resourceId)
        {
            var query = new GetResourceByIdQuery()
            {
                ResourceId = Guid.Parse(resourceId)
            };

            return Ok(await _mediator.Send(query));
        }

        /// <summary>
        /// Creates / registers a new resource
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost()]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateResourceCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Updates an existing Resource
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut()]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> Update([FromBody] UpdateResourceCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Deletes an existing project
        /// </summary>
        /// <param name="resourceId"></param>
        /// <returns></returns>
        [HttpDelete("{resourceId}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> Delete(string resourceId)
        {
            var command = new DeleteResourceCommand()
            {
                ResourceId = Guid.Parse(resourceId)
            };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
