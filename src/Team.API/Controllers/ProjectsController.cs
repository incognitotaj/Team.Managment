using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Team.API.Controllers
{
    public class ProjectsController : BaseApiController
    {
        [HttpGet()]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
