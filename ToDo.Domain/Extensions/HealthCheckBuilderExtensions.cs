using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Domain.HealthCheck;

namespace ToDo.Domain.Extensions
{
    public static class HealthCheckBuilderExtensions
    {
        public static IHealthChecksBuilder AddSelfCheck(this IHealthChecksBuilder builder, string name, HealthStatus? failureStatus = null, IEnumerable<string> tags = null)
        {
            // Register a check of type SelfHealthCheck
            builder.AddCheck<SelfHealthCheck>(name, failureStatus ?? HealthStatus.Degraded, tags);

            return builder;
        }
    }
}
