using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ToDo.Domain.Commands;
using ToDo.Domain.Entities;
using ToDo.Domain.Repositories.Interfaces;

namespace ToDo.Domain.Handlers
{

    public class GetUndoneForTodayToDoCommandHandler : IRequestHandler<GetUndoneForTodayToDoCommand, IEnumerable<TodoItem>>
    {
        private readonly ITodoRepository _todoRepository;

        public GetUndoneForTodayToDoCommandHandler(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<IEnumerable<TodoItem>> Handle(GetUndoneForTodayToDoCommand request, CancellationToken cancellationToken)
        {
            var dados = await _todoRepository.Search(a => a.User == request.User && a.Done == false && a.Date == DateTime.Now.Date);
            return await Task.FromResult(dados);
        }
    }
}
