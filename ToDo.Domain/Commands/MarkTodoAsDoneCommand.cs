using System;
using System.Diagnostics.Contracts;
using System.Windows.Input;
using MediatR;

namespace ToDo.Domain.Commands
{
    public class MarkTodoAsDoneCommand : IRequest<string>
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