using FluentValidation;
using ToDo.Domain.Commands;

namespace ToDo.Domain.Validators.Commands
{
    public class MarkToDoAsDoneCommandValidator : AbstractValidator<MarkTodoAsDoneCommand>
    {
        public MarkToDoAsDoneCommandValidator()
        {   
            RuleFor(a => a.User).NotEmpty().MinimumLength(3).WithMessage("Usuario deve ter pelo menos 3 caracteres");
            RuleFor(a => a.Id).NotEmpty().WithMessage("Id Não pode ser nulo");
        }
    }
}