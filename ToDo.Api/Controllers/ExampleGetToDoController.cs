using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDo.Domain.Services;

namespace ToDo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class ExampleGetToDoController : ControllerBase
    {
        private readonly IExampleGetToDoService _exampleGetToDoService;

        public ExampleGetToDoController(IExampleGetToDoService exampleGetToDoService)
        {
            _exampleGetToDoService = exampleGetToDoService;
        }

        [Route("GetToDos")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetToDos()
        {
            var response = await _exampleGetToDoService.GetAllToDo();
                
            return Ok(response.ToList());
        }
    }
}