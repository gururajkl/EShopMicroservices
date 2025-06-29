namespace Ordering.Application.Orders.Commands.CreateOrder;

public record CreateOrderResult(Guid Id);

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.Order.OrderName).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Order.CustomerId).NotEmpty().WithMessage("Customer Id is required");
        RuleFor(x => x.Order.OrderItem).NotEmpty().WithMessage("Order Item is required");
    }
}

public record CreateOrderCommand(OrderDto Order) : ICommand<CreateOrderResult>;
