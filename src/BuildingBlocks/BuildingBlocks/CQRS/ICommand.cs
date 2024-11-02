using MediatR;

namespace BuildingBlocks.CQRS;

/// <summary>
/// Marker interface representing a command with no return type.
/// Inherits from <see cref="ICommand{Unit}"/> where <c>Unit</c> is a void type for MediatR.
/// </summary>
public interface ICommand : ICommand<Unit>
{ }

/// <summary>
/// Interface representing a command with a specific return type.
/// Inherits from MediatR's <see cref="IRequest{TResponse}"/> to allow for CQRS-based request handling.
/// </summary>
/// <typeparam name="TResponse"></typeparam>
public interface ICommand<out TResponse> : IRequest<TResponse>
{ }
