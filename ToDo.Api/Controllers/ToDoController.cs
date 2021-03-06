using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDo.Domain.Commands;
using ToDo.Domain.Notification;
using Microsoft.Extensions.Logging;
using ToDo.Domain.Commands.ToDoCommands;
using ToDo.Domain.Services;

namespace ToDo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
  
    public class ToDoController : ControllerBase
    {
        private readonly IMediator _bus;
        private readonly IDomainNotificationContext _notificationContext;
        private readonly ILogger<ToDoController> _logger;
        private readonly IExampleGetToDoService _exampleGetToDoService;

        public ToDoController(IMediator bus, IDomainNotificationContext notificationContext,
            ILogger<ToDoController> logger, IExampleGetToDoService exampleGetToDoService)
        {
            _bus = bus;
            _notificationContext = notificationContext;
            _logger = logger;
            _exampleGetToDoService = exampleGetToDoService;
        }

        [Route("GetAll")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var response = await _bus.Send(new GetAllToDoCommand("Alessandro"));
            _logger.LogInformation($"Success to get all todos");

            if (_notificationContext.HasErrorNotifications)
            {
                var notifications = _notificationContext.GetErrorNotifications();
                var message = string.Join(", ", notifications.Select(x => x.Value));
                _logger.LogError(message);
                return BadRequest(message);
            }

            return Ok(response);
        }

        [Route("GetAllWithRefit")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllWithRefit()
        {
            var response = await  _exampleGetToDoService.GetAllToDo();

            return Ok(response);
        }


        [Route("done")]
        [HttpGet]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> GetAllDone()
        {
            var response = await _bus.Send(new GetAllDoneToDoCommand(User.Claims.FirstOrDefault()?.Value));
            _logger.LogInformation($"Success to get all done todos");

            if (_notificationContext.HasErrorNotifications)
            {
                var notifications = _notificationContext.GetErrorNotifications();
                var message = string.Join(", ", notifications.Select(x => x.Value));
                _logger.LogError(message);
                return BadRequest(message);
            }

            return Ok(response);
        }

        [Route("undone")]
        [HttpGet]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> GetAllUndone()
        {
            var response = await _bus.Send(new GetAllUndoneToDoCommand(User.Claims.FirstOrDefault()?.Value));
            _logger.LogInformation($"Success to get all undone todos");

            if (_notificationContext.HasErrorNotifications)
            {
                var notifications = _notificationContext.GetErrorNotifications();
                var message = string.Join(", ", notifications.Select(x => x.Value));
                _logger.LogError(message);
                return BadRequest(message);
            }

            return Ok(response);
        }

        [Route("done/today")]
        [HttpGet]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> GetDoneForToday()
        {
            var response = await _bus.Send(new GetDoneForTodayToDoCommand(User.Claims.FirstOrDefault()?.Value));
            _logger.LogInformation($"Success to get all done for today todos");

            if (_notificationContext.HasErrorNotifications)
            {
                var notifications = _notificationContext.GetErrorNotifications();
                var message = string.Join(", ", notifications.Select(x => x.Value));
                _logger.LogError(message);
                return BadRequest(message);
            }

            return Ok(response);
        }

        [Route("undone/today")]
        [HttpGet]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> GetInactiveForToday()
        {
            var response = await _bus.Send(new GetUndoneForTodayToDoCommand(User.Claims.FirstOrDefault()?.Value));
            _logger.LogInformation($"Success to get all undone todos");

            if (_notificationContext.HasErrorNotifications)
            {
                var notifications = _notificationContext.GetErrorNotifications();
                var message = string.Join(", ", notifications.Select(x => x.Value));
                _logger.LogError(message);
                return BadRequest(message);
            }

            return Ok(response);
        }

        [Route("done/tomorrow")]
        [HttpGet]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> GetDoneForTomorrow()
        {
            var response = await _bus.Send(new GetDoneForTomorrowToDoCommand(User.Claims.FirstOrDefault()?.Value));
            _logger.LogInformation($"Success to get done for tomorrow todos");

            if (_notificationContext.HasErrorNotifications)
            {
                var notifications = _notificationContext.GetErrorNotifications();
                var message = string.Join(", ", notifications.Select(x => x.Value));
                _logger.LogError(message);
                return BadRequest(message);
            }

            return Ok(response);
        }

        [Route("undone/tomorrow")]
        [HttpGet]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> GetUndoneForTomorrow()
        {
            var response = await _bus.Send(new GetUndoneForTomorrowToDoCommand(User.Claims.FirstOrDefault()?.Value));
            _logger.LogInformation($"Success to get all undone for tomorrow todos");

            if (_notificationContext.HasErrorNotifications)
            {
                var notifications = _notificationContext.GetErrorNotifications();
                var message = string.Join(", ", notifications.Select(x => x.Value));
                _logger.LogError(message);
                return BadRequest(message);
            }

            return Ok(response);
        }

        [Route("")]
        [HttpPost]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> Create([FromBody] CreateTodoCommand command)
        {
            command.User = User.Claims.FirstOrDefault()?.Value;

            var ret = await _bus.Send(command);
            _logger.LogInformation($"Success to create todo");

            if (_notificationContext.HasErrorNotifications)
            {
                var notifications = _notificationContext.GetErrorNotifications();
                var message = string.Join(", ", notifications.Select(x => x.Value));
                _logger.LogError(message);
                return BadRequest(message);
            }

            return Ok(ret);
        }

        [Route("")]
        [HttpPut]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> Update([FromBody] UpdateTodoCommand command)
        {
            command.User = User.Claims.FirstOrDefault()?.Value;

            var ret = await _bus.Send(command);
            _logger.LogInformation($"Success to update todo");

            if (_notificationContext.HasErrorNotifications)
            {
                var notifications = _notificationContext.GetErrorNotifications();
                var message = string.Join(", ", notifications.Select(x => x.Value));
                return BadRequest(message);
            }

            return Ok(ret);
        }

        [Route("mark-as-done")]
        [HttpPut]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> MarkAsDone([FromBody] MarkTodoAsDoneCommand command)
        {
            var response = await _bus.Send(new MarkTodoAsDoneCommand(command.Id, User.Claims.FirstOrDefault()?.Value));
            _logger.LogInformation($"Success to mark todo as done");

            if (_notificationContext.HasErrorNotifications)
            {
                var notifications = _notificationContext.GetErrorNotifications();
                var message = string.Join(", ", notifications.Select(x => x.Value));
                _logger.LogError(message);
                return BadRequest(message);
            }

            return Ok(response);
        }

        [Route("mark-as-undone")]
        [HttpPut]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> MarkAsUndone([FromBody] MarkTodoAsUndoneCommand command)
        {
            var response =
                await _bus.Send(new MarkTodoAsUndoneCommand(command.Id, User.Claims.FirstOrDefault()?.Value));
            _logger.LogInformation($"Success to mark todo as undone");

            if (_notificationContext.HasErrorNotifications)
            {
                var notifications = _notificationContext.GetErrorNotifications();
                var message = string.Join(", ", notifications.Select(x => x.Value));
                _logger.LogError(message);
                return BadRequest(message);
            }

            return Ok(response);
        }
    }
}