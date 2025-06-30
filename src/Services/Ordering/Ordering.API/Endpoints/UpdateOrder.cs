using Ordering.Application.Orders.Commands.UpdateOrder;

namespace Ordering.API.Endpoints;

public record UpdateOrderRequest(OrderDto OrderDto);
public record UpdateOrderResponse(bool IsSuccess);

public class UpdateOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/orders", async (UpdateOrderRequest request, ISender sender) =>
        {
            var commandRequest = request.Adapt<UpdateOrderCommand>();
            var commandResult = await sender.Send(commandRequest);
            var response = commandResult.Adapt<UpdateOrderResponse>();
            return Results.Ok(response);
        })
        .WithName("UpdateOrder")
        .WithDescription("Update Order")
        .WithSummary("Update order with CQRS pattern")
        .Produces<UpdateOrderResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound);
    }
}
