using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Team.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
    }
}
