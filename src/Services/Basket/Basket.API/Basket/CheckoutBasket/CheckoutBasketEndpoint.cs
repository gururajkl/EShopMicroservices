﻿using Basket.API.Dtos;

namespace Basket.API.Basket.CheckoutBasket;

public record CheckoutBasketRequest(BasketCheckoutDto BasketCheckoutDto);
public record CheckoutBasketResponse(bool IsSuccess);

public class CheckoutBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket/checkout", async (CheckoutBasketRequest request, ISender sender) =>
        {
            var command = request.Adapt<CheckoutBasketCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<CheckoutBasketResponse>();
            return Results.Ok(response);
        })
        .WithName("CheckoutBasket")
        .WithDescription("Checkout Basket")
        .WithSummary("Checkout basket with CQRS pattern")
        .Produces<CheckoutBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}
