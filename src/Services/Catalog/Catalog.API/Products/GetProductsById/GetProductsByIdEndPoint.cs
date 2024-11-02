namespace Catalog.API.Products.GetProductsById;

public record GetProductByIdRequest(Guid Id);
public record GetProductByIdResponse(Product Product);

public class GetProductsByIdEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetProductByIdQuery(id));
            var response = result.Adapt<GetProductByIdResponse>();
            return Results.Ok(response);
        }).WithName("GetProductById").WithDescription("Get Product By Id").WithSummary("Get Product By Id")
          .Produces<GetProductByIdResponse>(StatusCodes.Status201Created).ProducesProblem(StatusCodes.Status400BadRequest);
    }
}
