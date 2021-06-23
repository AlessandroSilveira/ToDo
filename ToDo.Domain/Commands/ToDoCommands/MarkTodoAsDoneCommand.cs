using System;
using MediatR;

namespace ToDo.Domain.Commands.ToDoCommands
{
    public class MarkTodoAsDoneCommand : IRequest<GenericCommandResult>
    {
        public MarkTodoAsDoneCommand() { }

        public MarkTodoAsDoneCommand(Guid id, string user)
        {
            Id = id;
            User = user;
        }

        public Guid Id { get; set; }
        public string User { get; set; }

       
    }
}