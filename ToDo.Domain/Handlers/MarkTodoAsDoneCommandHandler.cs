using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ToDo.Domain.Commands;
using ToDo.Domain.Repositories.Interfaces;

namespace ToDo.Domain.Handlers
{   
    public class MarkTodoAsDoneCommandHandler : IRequestHandler<MarkTodoAsDoneCommand, GenericCommandResult>
    {
        private readonly ITodoRepository _repository;

        public MarkTodoAsDoneCommandHandler(ITodoRepository repository)
        {
            _repository = repository;
        }

        public async Task<GenericCommandResult> Handle(MarkTodoAsDoneCommand request, CancellationToken cancellationToken)
        {
            var todo = await _repository.GetById(request.Id);
            todo.MarkAsDone();

            await _repository.Update(todo);
            return new GenericCommandResult(true, "Tarefa salva", request);
        }
    }
}
