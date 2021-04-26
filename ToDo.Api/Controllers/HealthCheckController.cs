using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDo.Api.Controllers
{
    [Route("v1/[controller]")]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet]
        [Route("HealthCheck")]
        [AllowAnonymous]
        public async Task<IActionResult> HealthCheck()
        {
            return Ok();
        }
    }
}
