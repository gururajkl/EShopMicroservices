using MediatR;

namespace BuildingBlocks.CQRS;

/// <summary>
/// Interface for a query handler with a specific return type.
/// Inherits from MediatR's <see cref="IRequestHandler{TQuery, TResponse}"/> for handling CQRS-based queries.
/// </summary>
/// <typeparam name="TQuery">The type of query handled, constrained to queries that return <typeparamref name="TResponse"/>.</typeparam>
/// <typeparam name="TResponse">The type of response returned by the query handler, constrained to non-nullable types.</typeparam>
public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
    where TResponse : notnull
{ }
