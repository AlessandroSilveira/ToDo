using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDo.Domain.Commands;
using ToDo.Domain.Services;

namespace ToDo.Api.Controllers
{
    [ApiController]
    [Route("api/todos")]
    [Authorize]

    public class ExampleGetToDoController : ControllerBase
    {
        private readonly IExampleGetToDoService _exampleGetToDoService;

        public ExampleGetToDoController(IExampleGetToDoService exampleGetToDoService)
        {
            _exampleGetToDoService = exampleGetToDoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetToDos()
        {
            var response = await _exampleGetToDoService.GetAllToDo();
            return Ok(response);
        }
    }
}