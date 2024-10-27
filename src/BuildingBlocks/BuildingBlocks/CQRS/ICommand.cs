using MediatR;

namespace BuildingBlocks.CQRS;

public interface ICommand : ICommand<Unit> // Unit is a void type for MedatR.
{ }

public interface ICommand<out TResponse> : IRequest<TResponse>
{ }
