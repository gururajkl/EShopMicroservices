namespace Catalog.API.Products.GetProductByCategory;

public record GetProductByCategoryRequest(string Category);
public record GetProductByCategoryResponse(IEnumerable<Product> Products);

public class GetProductByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{categoryName}", async (string categoryName, ISender sender) =>
        {
            var result = await sender.Send(new GetProductByCategoryQuery(categoryName));
            var response = result.Adapt<GetProductByCategoryResponse>();
            return Results.Ok(response);
        }).WithName("GetProductByCategory") // Response descriptions and details.
          .WithDescription("Get Product By Category")
          .WithSummary("Get Product By Category")
          .Produces<GetProductByCategoryResponse>(StatusCodes.Status201Created)
          .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}
