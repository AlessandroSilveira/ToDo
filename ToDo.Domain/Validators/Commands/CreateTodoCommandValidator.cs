using FluentValidation;
using ToDo.Domain.Commands;
using ToDo.Domain.Commands.ToDoCommands;

namespace ToDo.Domain.Validators.Commands
{
    public class CreateTodoCommandValidator : AbstractValidator<CreateTodoCommand>
    {
        public CreateTodoCommandValidator()
        {
            RuleFor(a => a.Title).NotEmpty().MinimumLength(3).WithMessage("Titulo deve ter pelo menos 3 caracteres");
            RuleFor(a => a.User).NotEmpty().MinimumLength(3).WithMessage("Usuario deve ter pelo menos 3 caracteres");
            RuleFor(a => a.Date).NotEmpty().WithMessage("Data não pode ser vazio");
        }
    }
}