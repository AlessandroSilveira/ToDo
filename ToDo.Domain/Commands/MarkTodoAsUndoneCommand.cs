using System;
using System.Diagnostics.Contracts;
using MediatR;

namespace ToDo.Domain.Commands
{
    public class MarkTodoAsUndoneCommand : IRequest<GenericCommandResult>
    {
        public MarkTodoAsUndoneCommand() { }

        public MarkTodoAsUndoneCommand(Guid id, string user)
        {
            Id = id;
            User = user;
        }

        public Guid Id { get; set; }
        public string User { get; set; }
    }
}
