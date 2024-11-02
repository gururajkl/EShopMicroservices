using MediatR;

namespace BuildingBlocks.CQRS;

/// <summary>
/// Marker interface for a command handler with no return type.
/// Inherits from <see cref="ICommandHandler{TCommand, Unit}"/> where <c>Unit</c> is a void type for MediatR.
/// </summary>
/// <typeparam name="TCommand">The type of command handled, constrained to commands that return <c>Unit</c>.</typeparam>
public interface ICommandHandler<in TCommand> : ICommandHandler<TCommand, Unit>
    where TCommand : ICommand<Unit>
{ }

/// <summary>
/// Interface for a command handler with a specific return type.
/// Inherits from MediatR's <see cref="IRequestHandler{TCommand, TResponse}"/> to handle CQRS-based command processing.
/// </summary>
/// <typeparam name="TCommand">The type of command handled, constrained to commands that return <typeparamref name="TResponse"/>.</typeparam>
/// <typeparam name="TResponse">The type of response returned by the command handler, constrained to non-nullable types.</typeparam>
public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
    where TResponse : notnull
{ }
