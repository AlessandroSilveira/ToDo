using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ToDo.Domain.Commands;
using ToDo.Domain.Commands.ToDoCommands;
using ToDo.Domain.Entities;
using ToDo.Domain.Repositories.Interfaces;

namespace ToDo.Domain.Handlers.ToDoHandles
{

    public class GetAllUndoneToDoCommandHandler : IRequestHandler<GetAllUndoneToDoCommand, IEnumerable<TodoItem>>
    {
        private readonly ITodoRepository _todoRepository;

        public GetAllUndoneToDoCommandHandler(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<IEnumerable<TodoItem>> Handle(GetAllUndoneToDoCommand request, CancellationToken cancellationToken)
        {
            var dados = await _todoRepository.Search(a => a.User == request.User && a.Done == false);
            return await Task.FromResult(dados);
        }
    }
}
