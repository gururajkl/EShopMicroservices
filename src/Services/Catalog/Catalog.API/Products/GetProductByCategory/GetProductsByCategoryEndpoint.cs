namespace Catalog.API.Products.GetProductByCategory;

public record GetProductsByCategoryResponse(IEnumerable<Product> Products);

public class GetProductsByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
        {
            var result = await sender.Send(new GetProductsByCategoryQuery(category));
            var response = result.Adapt<GetProductsByCategoryResponse>();
            return response;
        })
        .WithName("GetProductByCategory")
        .WithDescription("Get product by Category")
        .WithSummary("Get product by Category using CQRS pattern")
        .Produces<GetProductsByCategoryResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}
