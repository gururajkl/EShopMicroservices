namespace Catalog.API.Products.GetProducts;

public record GetProductsResponse(IEnumerable<Product> Products);

public class GetProductsEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (ISender sender) =>
        {
            var result = await sender.Send(new GetProductsQuery());
            // Using Mapster to map the objects.
            var response = result.Adapt<GetProductsResponse>();
            return Results.Ok(response);
        }).WithName("GetProduct").WithDescription("Get Product").WithSummary("Get Product")
          .Produces<GetProductsResponse>(StatusCodes.Status201Created).ProducesProblem(StatusCodes.Status400BadRequest);
    }
}
