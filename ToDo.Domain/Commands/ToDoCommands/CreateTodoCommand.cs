using System;
using MediatR;

namespace ToDo.Domain.Commands.ToDoCommands
{
    public class CreateTodoCommand  :  IRequest<GenericCommandResult>
    {
        public CreateTodoCommand() { }

        public CreateTodoCommand(string title, string user, DateTime date)
        {
            Title = title;
            User = user;
            Date = date;
        }

        public string Title { get; set; }
        public string User { get; set; }
        public DateTime Date { get; set; }

    }
}