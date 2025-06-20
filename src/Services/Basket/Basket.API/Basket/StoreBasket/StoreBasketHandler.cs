using Discount.Grpc;

namespace Basket.API.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
public record StoreBasketResult(string UserName);

// Validation for the command.
public class StoreBasketValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketValidator()
    {
        RuleFor(r => r.Cart.UserName).NotEmpty().WithMessage("Username is required");
        RuleFor(r => r.Cart).NotNull().WithMessage("Cart cannot be null");
    }
}

public class StoreBasketCommandHandler(IBasketRepository repository, DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient,
    ILogger<StoreBasketCommandHandler> logger)
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        // Communicate with Grpc service to calculate the discount of each product.
        foreach (var item in command.Cart.Items)
        {
            var coupon = await discountProtoServiceClient.GetDiscountAsync(new GetDiscountRequest() { ProductName = item.ProductName },
                cancellationToken: cancellationToken);

            if (coupon.Amount <= 0)
            {
                logger.LogInformation("No discount will be applied for {0}", coupon.ProductName);
            }

            // After getting the coupon of the product calculate the total price.
            item.Price -= coupon.Amount;
        }

        await repository.StoreBasket(command.Cart, cancellationToken);

        return new StoreBasketResult(command.Cart.UserName);
    }
}
