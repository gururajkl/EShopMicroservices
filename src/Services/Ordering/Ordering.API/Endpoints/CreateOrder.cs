using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.API.Endpoints;

public record CreateOrderRequest(OrderDto Order);
public record CreateOrderResponse(Guid Id);

public class CreateOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/orders", async (CreateOrderRequest request, ISender sender) =>
        {
            var createOrderCommand = request.Adapt<CreateOrderCommand>();
            CreateOrderResult createOrderResult = await sender.Send(createOrderCommand);
            var response = createOrderResult.Adapt<CreateOrderResponse>();
            return Results.Created($"/orders/{response.Id}", response);
        })
        .WithName("CreateOrder")
        .WithDescription("Create Order")
        .WithSummary("Create with CQRS pattern")
        .Produces<CreateOrderResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound);
    }
}
