namespace Catalog.API.Products.GetProducts;

public record GetProductsResponse(IEnumerable<Product> Products);

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (ISender sender) =>
        {
            GetProductsResult getProductsResult = await sender.Send(new GetProductsQuery());
            GetProductsResponse getProductsResponse = getProductsResult.Adapt<GetProductsResponse>();
            return Results.Ok(getProductsResponse);
        })
        .WithName("GetProducts")
        .WithDescription("Get products")
        .WithSummary("Get products with CQRS pattern")
        .Produces<GetProductsResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}
