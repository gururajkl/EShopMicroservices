namespace Catalog.API.Products.GetProducts;

public record GetProductRequest(int? PageNumber = 1, int? PageSize = 10);
public record GetProductsResponse(IEnumerable<Product> Products);

public class GetProductsEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async ([AsParameters] GetProductRequest request, ISender sender) =>
        {
            var query = request.Adapt<GetProductsQuery>();
            var result = await sender.Send(query);
            // Using Mapster to map the objects.
            var response = result.Adapt<GetProductsResponse>();
            return Results.Ok(response);
        }).WithName("GetProduct").WithDescription("Get Product").WithSummary("Get Product")
          .Produces<GetProductsResponse>(StatusCodes.Status201Created).ProducesProblem(StatusCodes.Status400BadRequest);
    }
}
