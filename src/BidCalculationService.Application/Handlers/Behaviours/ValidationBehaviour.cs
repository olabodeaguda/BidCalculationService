using FluentValidation;
using MediatR;

namespace BidCalculationService.Application.Handlers.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validator;
        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validator)
        {
            _validator = validator;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validator.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var valiationResult = await Task.WhenAll(_validator.Select(_ => _.ValidateAsync(context, cancellationToken)));
                var failures = valiationResult.SelectMany(_ => _.Errors).Where(_ => _ != null).ToList();
                if (failures.Count != 0)
                    throw new ValidationException(failures);
            }

            return await next();
        }
    }
}
