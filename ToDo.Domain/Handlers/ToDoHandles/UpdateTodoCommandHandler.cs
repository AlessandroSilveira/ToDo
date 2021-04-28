using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ToDo.Domain.Commands;
using ToDo.Domain.Commands.ToDoCommands;
using ToDo.Domain.Repositories.Interfaces;

namespace ToDo.Domain.Handlers.ToDoHandles
{

    public class UpdateTodoCommandHandler : IRequestHandler<UpdateTodoCommand, GenericCommandResult>
    {
        private readonly ITodoRepository _repository;

        public UpdateTodoCommandHandler(ITodoRepository repository)
        {
            _repository = repository;
        }

        public async Task<GenericCommandResult> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = await _repository.GetById(request.Id);
            todo.UpdateTitle(request.Title);

            await _repository.Update(todo);
            return new GenericCommandResult(true, "Tarefa salva", request);
        }

    }
}
