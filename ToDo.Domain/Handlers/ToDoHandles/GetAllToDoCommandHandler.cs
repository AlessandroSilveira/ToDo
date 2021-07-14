using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ToDo.Domain.Commands;
using ToDo.Domain.Commands.ToDoCommands;
using ToDo.Domain.Entities;
using ToDo.Domain.Repositories.Interfaces;

namespace ToDo.Domain.Handlers.ToDoHandles
{
    public class GetAllToDoCommandHandler : IRequestHandler<GetAllToDoCommand, IEnumerable<TodoItem>>
    {
        private readonly ITodoRepository _todoRepository;

        public GetAllToDoCommandHandler(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<IEnumerable<TodoItem>> Handle(GetAllToDoCommand request, CancellationToken cancellationToken)
        {
            var dados = await _todoRepository.Search(a => a.User == request.User);
            
            return await Task.FromResult(dados);
        }
    }
}