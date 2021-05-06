using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ToDo.Api.Controllers
{
    [Route("api/[controller]")]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet]
        [Route("HealthCheck")]
        [AllowAnonymous]
        public IActionResult HealthCheck()
        {
            return Ok();
        }
    }
}
