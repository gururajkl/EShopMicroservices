namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);

// Validate CreateProductCommand before creating the product.
// As this class implemented AbstractValidator, FluentValidator registers this in it.
public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(r => r.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(r => r.Category).NotEmpty().WithMessage("Category is required");
        RuleFor(r => r.ImageFile).NotEmpty().WithMessage("Category is required");
        RuleFor(r => r.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
    }
}

// Handler class.
internal class CreateProductCommandHandler(IDocumentSession session)
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        // Create product entity from command object.
        var product = new Product()
        {
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price,
        };

        // Save to DB.
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);

        // Return result.
        return new CreateProductResult(product.Id);
    }
}
