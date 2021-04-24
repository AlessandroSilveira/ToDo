using System;
using ToDo.Domain.HealthCheck.Interface;

namespace ToDo.Domain.HealthCheck
{
    public class ToDoCheckService : IToDoCheckService
    {
        public bool IsHealthy()
        {
            return new Random().NextDouble() > 0.5;
        }
    }
}