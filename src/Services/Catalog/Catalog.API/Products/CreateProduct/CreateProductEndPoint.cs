namespace Catalog.API.Products.CreateProduct;

public record CreateProductRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price);
public record CreateProductResonpse(Guid Id);

public class CreateProductEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
        {
            // Using Mapster to map the objects.
            var command = request.Adapt<CreateProductCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<CreateProductResonpse>();
            return Results.Created($"/products/{response.Id}", response);
        }).WithName("CreateProduct").WithDescription("Create Product").WithSummary("Create Product")
          .Produces<CreateProductResonpse>(StatusCodes.Status201Created).ProducesProblem(StatusCodes.Status400BadRequest);
    }
}
