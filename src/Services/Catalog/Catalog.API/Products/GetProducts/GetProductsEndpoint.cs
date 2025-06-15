namespace Catalog.API.Products.GetProducts;

public record GetProductsRequest(int? PageNumber = 1, int? PageSize = 10);
public record GetProductsResponse(IEnumerable<Product> Products);

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async ([AsParameters] GetProductsRequest request, ISender sender) =>
        {
            var query = request.Adapt<GetProductsQuery>();
            GetProductsResult getProductsResult = await sender.Send(query);
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
