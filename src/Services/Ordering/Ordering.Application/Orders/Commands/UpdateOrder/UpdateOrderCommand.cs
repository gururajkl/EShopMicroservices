namespace Ordering.Application.Orders.Commands.UpdateOrder;

public record UpdateOrderResult(bool IsSuccess);

public class UpdateOrderValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderValidator()
    {
        RuleFor(command => command.Order.Id).NotEmpty().WithMessage("Id is required.");
        RuleFor(command => command.Order.OrderName).NotEmpty().WithMessage("Order name cannot be empty.");
        RuleFor(command => command.Order.CustomerId).NotNull().WithMessage("Customer Id is required.");
    }
}

public record UpdateOrderCommand(OrderDto Order) : ICommand<UpdateOrderResult> { }
