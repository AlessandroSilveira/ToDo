using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDo.Domain.Commands;
using ToDo.Domain.Entities;
using Todo.Domain.Handlers;
using ToDo.Domain.Notification;
using ToDo.Domain.Repositories.Interfaces;
using ToDo.Domain.Services.Interfaces;

namespace ToDo.Api.Controllers
{
    [ApiController]
    [Route("v1/todos")]
    [Authorize]
    public class ToDoController : ControllerBase
    {
        private readonly ITodoService _todoService;
        private readonly IMediator _bus;
        private readonly IDomainNotificationContext _notificationContext;

        public ToDoController(ITodoService todoService, IMediator bus, IDomainNotificationContext notificationContext)
        {
            _todoService = todoService;
            _bus = bus;
            _notificationContext = notificationContext;
        }

        [Route("")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        { 
            var response = await _bus.Send(new GetAllToDoCommand(User.Claims.FirstOrDefault()?.Value));
            
            if (_notificationContext.HasErrorNotifications)
            {
                var notifications = _notificationContext.GetErrorNotifications();
                var message = string.Join(", ", notifications.Select(x => x.Value));
                return BadRequest(message);
            }
            
            return Ok(response);
        }

        [Route("done")]
        [HttpGet]
        public async Task<IActionResult> GetAllDone()
        {
            // var user = User.Claims.FirstOrDefault()?.Value;
            // return await _todoService.GetAllDone(user);
            var response = await _bus.Send(new GetAllDoneToDoCommand(User.Claims.FirstOrDefault()?.Value));
            
            if (_notificationContext.HasErrorNotifications)
            {
                var notifications = _notificationContext.GetErrorNotifications();
                var message = string.Join(", ", notifications.Select(x => x.Value));
                return BadRequest(message);
            }
            
            return Ok(response);
        }

        [Route("undone")]
        [HttpGet]
        public async Task<IEnumerable<TodoItem>> GetAllUndone([FromServices]ITodoRepository repository)
        {
            var user = User.Claims.FirstOrDefault()?.Value;
            return await _todoService.GetAllUndone(user);
        }

        [Route("done/today")]
        [HttpGet]
        public async Task<IEnumerable<TodoItem>> GetDoneForToday(
            [FromServices]ITodoRepository repository
        )
        {
            var user = User.Claims.FirstOrDefault()?.Value;
            return await _todoService.GetByPeriod(user, DateTime.Now.Date, true);
        }

        [Route("undone/today")]
        [HttpGet]
        public async Task<IEnumerable<TodoItem>> GetInactiveForToday(
            [FromServices]ITodoRepository repository
        )
        {
            var user = User.Claims.FirstOrDefault()?.Value;
            return await _todoService.GetByPeriod(user, DateTime.Now.Date, false);
        }

        [Route("done/tomorrow")]
        [HttpGet]
        public async Task<IEnumerable<TodoItem>> GetDoneForTomorrow([FromServices]ITodoRepository repository)
        {
            var user = User.Claims.FirstOrDefault()?.Value;
            return await _todoService.GetByPeriod(user, DateTime.Now.Date.AddDays(1), true);
        }

        [Route("undone/tomorrow")]
        [HttpGet]
        public async Task<IEnumerable<TodoItem>> GetUndoneForTomorrow(
            [FromServices]ITodoRepository repository
        )
        {
            var user = User.Claims.FirstOrDefault()?.Value;
            return  await _todoService.GetByPeriod(user, DateTime.Now.Date.AddDays(1), false);
        }

        [Route("")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateTodoCommand command, [FromServices]TodoHandler handler)
        {
             command.User = User.Claims.FirstOrDefault()?.Value;
            
            var ret = await _bus.Send(command);
            
            if (_notificationContext.HasErrorNotifications)
            {
                var notifications = _notificationContext.GetErrorNotifications();
                var message = string.Join(", ", notifications.Select(x => x.Value));
                return BadRequest(message);
            }
            
            return Ok(ret);
        }

        [Route("")]
        [HttpPut]
        public async Task<GenericCommandResult> Update([FromBody]UpdateTodoCommand command, [FromServices]TodoHandler handler)
        {
            command.User = User.Claims.FirstOrDefault()?.Value;
            return await handler.Handle(command);
        }

        [Route("mark-as-done")]
        [HttpPut]
        public async Task<GenericCommandResult> MarkAsDone([FromBody]MarkTodoAsDoneCommand command, [FromServices]TodoHandler handler)
        {
            command.User = User.Claims.FirstOrDefault()?.Value;
            return await handler.Handle(command);
        }

        [Route("mark-as-undone")]
        [HttpPut]
        public async Task<GenericCommandResult> MarkAsUndone([FromBody]MarkTodoAsUndoneCommand command, [FromServices]TodoHandler handler)
        {
            command.User = User.Claims.FirstOrDefault()?.Value;
            return await handler.Handle(command);
        }
    }
}