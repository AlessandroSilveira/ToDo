using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ToDo.Domain.Commands;
using ToDo.Domain.Notification;

namespace ToDo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ExampleGetToDoController : ControllerBase
    {

        private readonly IMediator _bus;
        private readonly IDomainNotificationContext _notificationContext;
        private readonly ILogger<ExampleGetToDoController> _logger;

        public ExampleGetToDoController(IMediator bus, IDomainNotificationContext notificationContext, ILogger<ExampleGetToDoController> logger)
        {
            _bus = bus;
            _notificationContext = notificationContext;
            _logger = logger;
        }

        [Route("GetToDos")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetToDos()
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
    }
}