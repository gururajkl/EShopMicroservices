namespace BuildingBlocks.CQRS;

// The 'out' keyword enables covariance, indicating that TResponse is used only as a return / output type, not as an input.
public interface ICommand<out TResponse> : IRequest<TResponse>
{
}

// Unit is a void type of MediatR.
public interface ICommand : ICommand<Unit>
{
}
