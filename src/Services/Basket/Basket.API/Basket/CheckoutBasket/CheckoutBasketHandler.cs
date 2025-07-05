using Basket.API.Dtos;
using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Basket.API.Basket.CheckoutBasket;

public record CheckoutBasketCommand(BasketCheckoutDto BasketCheckoutDto) : ICommand<CheckoutBasketResult>;
public record CheckoutBasketResult(bool IsSuccess);

public class CheckoutBasketCommandValidator : AbstractValidator<CheckoutBasketCommand>
{
    public CheckoutBasketCommandValidator()
    {
        RuleFor(x => x.BasketCheckoutDto).NotNull().WithMessage("Basket checkout dto cannot be null");
        RuleFor(x => x.BasketCheckoutDto.UserName).NotEmpty().WithMessage("Username is required");
    }
}

public class CheckoutBasketCommandHandler(IBasketRepository repository,
    IPublishEndpoint publishEndpoint) : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
{
    public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
    {
        // Get basket.
        var basket = await repository.GetBasket(command.BasketCheckoutDto.UserName, cancellationToken);
        if (basket is null)
        {
            return new(false);
        }

        // Publish message to rabbitmq.
        var eventMessage = command.BasketCheckoutDto.Adapt<BasketCheckoutEvent>();
        eventMessage.TotalPrice = basket.TotalPrice;

        await publishEndpoint.Publish(eventMessage, cancellationToken);

        // Delete basket after publishing.
        await repository.DeleteBasket(command.BasketCheckoutDto.UserName, cancellationToken);

        return new(true);
    }
}
