using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using ToDo.Domain.HealthCheck.Interface;

namespace ToDo.Domain.HealthCheck
{
    public class ToDoCheck : IHealthCheck
    {
        private readonly IToDoCheckService _toDoCheckService;

        public ToDoCheck(IToDoCheckService toDoCheckService)
        {
            _toDoCheckService = toDoCheckService;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            var result = _toDoCheckService.IsHealthy() ? HealthCheckResult.Healthy() : HealthCheckResult.Unhealthy();
            return Task.FromResult(result);
        }
    }

    
}