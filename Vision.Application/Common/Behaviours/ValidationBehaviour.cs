using FluentValidation;
using MediatR;

namespace Vision.Application.Common.Behaviours
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TResponse : class, new()
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
            var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

            if (failures.Count != 0)
            {
                var response = new TResponse();
                var warningResult = new WarningResult
                {
                    Title = "Validation Failed",
                    // Only include the first validation error message
                    Message = failures.First().ErrorMessage
                };

                var warningResultProperty = typeof(TResponse).GetProperty("WarningResult");
                if (warningResultProperty != null && warningResultProperty.PropertyType == typeof(WarningResult))
                {
                    warningResultProperty.SetValue(response, warningResult);
                }

                return response;
            }

            return await next();
        }
    }
}