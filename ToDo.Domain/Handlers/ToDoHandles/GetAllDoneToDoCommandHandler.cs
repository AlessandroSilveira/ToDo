using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ToDo.Domain.Commands;
using ToDo.Domain.Commands.ToDoCommands;
using ToDo.Domain.Entities;
using ToDo.Domain.Repositories.Interfaces;

namespace ToDo.Domain.Handlers.ToDoHandles
{
    public class GetAllDoneToDoCommandHandler : IRequestHandler<GetAllDoneToDoCommand, IEnumerable<TodoItem>>
    {
        private readonly ITodoRepository _todoRepository;

        public GetAllDoneToDoCommandHandler(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<IEnumerable<TodoItem>> Handle(GetAllDoneToDoCommand request, CancellationToken cancellationToken)
        {
           return await _todoRepository.Search(a => a.User == request.User && a.Done);
          
        }
    }
}