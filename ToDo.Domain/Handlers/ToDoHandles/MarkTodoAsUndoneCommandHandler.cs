using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ToDo.Domain.Commands;
using ToDo.Domain.Commands.ToDoCommands;
using ToDo.Domain.Repositories.Interfaces;

namespace ToDo.Domain.Handlers.ToDoHandles
{

    public class MarkTodoAsUndoneCommandHandler : IRequestHandler<MarkTodoAsUndoneCommand, GenericCommandResult>
    {
        private readonly ITodoRepository _repository;

        public MarkTodoAsUndoneCommandHandler(ITodoRepository repository)
        {
            _repository = repository;
        }

        public async Task<GenericCommandResult> Handle(MarkTodoAsUndoneCommand request, CancellationToken cancellationToken)
        {
            var todo = await _repository.GetById(request.Id);
            todo.MarkAsUndone();

            await _repository.Update(todo);
            return new GenericCommandResult(true, "Tarefa salva", request);
        }
    }
}
