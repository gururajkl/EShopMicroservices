using MediatR;

namespace BuildingBlocks.CQRS;

/// <summary>
/// Interface representing a query with a specific return type.
/// Inherits from MediatR's <see cref="IRequest{TResponse}"/>.
/// </summary>
/// <typeparam name="TResponse">The type of the response returned by the query handler, constrained to non-nullable types.</typeparam>
public interface IQuery<out TResponse> : IRequest<TResponse> where TResponse : notnull
{ }
