using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ToDo.Domain.Commands;
using ToDo.Domain.Entities;
using ToDo.Domain.Repositories.Interfaces;

namespace ToDo.Domain.Handlers
{
    public class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommand, GenericCommandResult>
    {
        private readonly ITodoRepository _repository;
        
        public CreateTodoCommandHandler(ITodoRepository repository)
        {
            _repository = repository;
        }
       

        async Task<GenericCommandResult> IRequestHandler<CreateTodoCommand, GenericCommandResult>.Handle(CreateTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = new TodoItem(request.Title, false, request.Date, request.User);
            await _repository.Add(todo);
            return new GenericCommandResult(true, "Tarefa salva", todo);
        }
    }
}