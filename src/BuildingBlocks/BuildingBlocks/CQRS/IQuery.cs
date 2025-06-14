namespace BuildingBlocks.CQRS;

// The 'out' keyword enables covariance, indicating that TResponse is used only as a return / output type, not as an input.
public interface IQuery<out TResponse> : IRequest<TResponse> where TResponse : notnull { }
