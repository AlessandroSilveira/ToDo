﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ToDo.Domain.Commands;
using ToDo.Domain.Entities;
using ToDo.Domain.Repositories.Interfaces;

namespace ToDo.Domain.Handlers
{
    public class GetDoneForTomorrowToDoCommandHandler : IRequestHandler<GetDoneForTomorrowToDoCommand, IEnumerable<TodoItem>>
    {
        private readonly ITodoRepository _todoRepository;

        public GetDoneForTomorrowToDoCommandHandler(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<IEnumerable<TodoItem>> Handle(GetDoneForTomorrowToDoCommand request, CancellationToken cancellationToken)
        {
            var dados = await _todoRepository.Search(a => a.User == request.User && a.Done == true && a.Date == DateTime.Now.AddDays(1));
            return await Task.FromResult(dados);
        }
    }
}
