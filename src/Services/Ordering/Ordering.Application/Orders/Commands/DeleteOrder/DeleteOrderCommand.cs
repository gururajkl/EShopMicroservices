namespace Ordering.Application.Orders.Commands.DeleteOrder;

public record DeletOrderCommand(Guid OrderId) : ICommand<DeleteOrderResult>;

public record DeleteOrderResult(bool IsSuccess);

public class DeleteOrderCommandValidator : AbstractValidator<DeletOrderCommand>
{
    public DeleteOrderCommandValidator()
    {
        RuleFor(command => command.OrderId).NotEmpty().WithMessage("Order Id is required.");
    }
}