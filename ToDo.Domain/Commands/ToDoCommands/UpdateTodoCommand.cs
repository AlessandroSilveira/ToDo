using System;
using System.Diagnostics.Contracts;
using MediatR;

namespace ToDo.Domain.Commands.ToDoCommands
{
    public class UpdateTodoCommand : IRequest<GenericCommandResult>
    {
        public UpdateTodoCommand() { }

        public UpdateTodoCommand(Guid id, string title, string user)
        {
            Id = id;
            Title = title;
            User = user;
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string User { get; set; }

    }
}