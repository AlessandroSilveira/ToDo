using System.Threading.Tasks;
using MediatR;
using ToDo.Domain.Commands;
using ToDo.Domain.Entities;
using ToDo.Domain.Repositories.Interfaces;

namespace Todo.Domain.Handlers
{
    public class TodoHandler :
        IRequest<CreateTodoCommand>,
        IRequest<UpdateTodoCommand>,
        IRequest<MarkTodoAsDoneCommand>,
        IRequest<MarkTodoAsUndoneCommand>
    {
        private readonly ITodoRepository _repository;

        public TodoHandler(ITodoRepository repository)
        {
            _repository = repository;
        }

        public async Task<GenericCommandResult> Handle(CreateTodoCommand command)
        {
            // Gera o TodoItem
            var todo = new TodoItem(command.Title, command.User, command.Date);

            // Salva no banco
            await _repository.Add(todo);

            // Retorna o resultado
            return new GenericCommandResult(true, "Tarefa salva", todo);
        }

        public async Task<GenericCommandResult> Handle(UpdateTodoCommand command)
        {
            // Recupera o TodoItem (Rehidratação)
            var todo = await _repository.GetById(command.Id);

            // Altera o título
            todo.UpdateTitle(command.Title);

            // Salva no banco
            await _repository.Update(todo);

            // Retorna o resultado
            return new GenericCommandResult(true, "Tarefa salva", todo);
        }

        public async Task<GenericCommandResult> Handle(MarkTodoAsDoneCommand command)
        {
            // Recupera o TodoItem
            var todo = await _repository.GetById(command.Id);

            // Altera o estado
            todo.MarkAsDone();

            // Salva no banco
            await _repository.Update(todo);

            // Retorna o resultado
            return new GenericCommandResult(true, "Tarefa salva", todo);
        }

        public async Task<GenericCommandResult> Handle(MarkTodoAsUndoneCommand command)
        {
            // Recupera o TodoItem
            var todo = await _repository.GetById(command.Id);

            // Altera o estado
            todo.MarkAsUndone();

            // Salva no banco
            await _repository.Update(todo);

            // Retorna o resultado
            return new GenericCommandResult(true, "Tarefa salva", todo);
        }
    }
}