using BuildingBlocks.CQRS;
using FluentValidation;
using MediatR;

namespace BuildingBlocks.Behaviors;

/// <summary>
/// A MediatR pipeline behavior that validates incoming requests of type <typeparamref name="TRequest"/> 
/// using registered validators before passing them to the request handler.
/// If any validation errors are detected, a <see cref="ValidationException"/> is thrown to halt processing.
/// </summary>
/// <typeparam name="TRequest">The type of the request being processed.</typeparam>
/// <typeparam name="TResponse">The type of the response expected from the request handler.</typeparam>
/// <param name="validators">A collection of validators for the <typeparamref name="TRequest"/> type.</param>
public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : ICommand<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        var failures = validationResults.Where(v => v.Errors.Any()).SelectMany(v => v.Errors).ToList();

        // If any validation errors are found, a validation error is thrown, halting further processing.
        if (failures.Any())
        {
            throw new ValidationException(failures);
        }

        // Call the next handler.
        return await next();
    }
}
