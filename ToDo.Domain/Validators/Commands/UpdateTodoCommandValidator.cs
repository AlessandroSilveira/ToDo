using FluentValidation;
using ToDo.Domain.Commands.ToDoCommands;

namespace ToDo.Domain.Validators.Commands
{
    public class UpdateTodoCommandValidator : AbstractValidator<UpdateTodoCommand>
    {
        public UpdateTodoCommandValidator()
        {
            RuleFor(a => a.Title).NotEmpty().MinimumLength(3).WithMessage("Titulo deve ter pelo menos 3 caracteres");
            RuleFor(a => a.User).NotEmpty().MinimumLength(3).WithMessage("Usuario deve ter pelo menos 3 caracteres");
            RuleFor(a => a.Id).NotEmpty().WithMessage("Id Não pode ser nulo");
        }
    }
}