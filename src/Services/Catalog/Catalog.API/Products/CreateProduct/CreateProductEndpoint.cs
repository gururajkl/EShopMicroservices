namespace Catalog.API.Products.CreateProduct;

public record CreateProductRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price);
public record CreateProductResponse(Guid Id);

public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
        {
            // Create Command for MediatR.
            CreateProductCommand command = request.Adapt<CreateProductCommand>();

            // Send the request so that handler will handle that.
            CreateProductResult createProductResult = await sender.Send(command);

            // Once done get the response and use Mapster to convert the object.
            CreateProductResponse response = createProductResult.Adapt<CreateProductResponse>();

            return Results.Created($"/products/{response.Id}", response);
        })
        .WithName("CreateProduct")
        .WithDescription("Creates product")
        .WithSummary("Creates product with CQRS pattern")
        .Produces<CreateProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}
