using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using ToDo.Domain.Notification;

namespace ToDo.Domain.Behaviors
{
    public class ValidationRequestBehavior<TRequest, Unit> : IPipelineBehavior<TRequest, Unit>
    {
        private readonly IEnumerable<IValidator> _validators;
        private readonly IDomainNotificationContext _notificationContext;
        
        public ValidationRequestBehavior(IEnumerable<IValidator> validators, IDomainNotificationContext notificationContext)
        {
            _validators = validators;
            _notificationContext = notificationContext;
        }
        
        public Task<Unit> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<Unit> next)
        {
            var failures = _validators
                .Select(v => v.Validate((IValidationContext)request))
                .SelectMany(x => x.Errors)
                .Where(f => f != null)
                .ToList();

            return failures.Any() ? Notify(failures) : next();
        }
        
        private Task<Unit> Notify(IEnumerable<ValidationFailure> failures)
        {
            var result = default(Unit);

            foreach (var failure in failures)
            {
                _notificationContext.NotifyError(failure.ErrorMessage);
            }

            return Task.FromResult(result);
        }
    }
}