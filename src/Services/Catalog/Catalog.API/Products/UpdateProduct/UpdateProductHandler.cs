namespace Catalog.API.Products.UpdateProduct;

/// <summary>
/// Command to update an existing product in the catalog.
/// Inherits from <see cref="ICommand{UpdateProductResult}"/> to specify a result type.
/// </summary>
/// <param name="Id">The unique identifier of the product to update.</param>
/// <param name="Name">The new name of the product.</param>
/// <param name="Category">The list of categories for the product.</param>
/// <param name="Description">The new description of the product.</param>
/// <param name="ImageFile">The new image file path or URL for the product.</param>
/// <param name="Price">The new price of the product.</param>
public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    : ICommand<UpdateProductResult>;

/// <summary>
/// Result of an update product operation, indicating success.
/// </summary>
/// <param name="IsSuccess">Indicates whether the product update operation succeeded.</param>
public record UpdateProductResult(bool IsSuccess);

// Class that class before calling the handler class for validating the Product object.
public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    /// <summary>
    /// Registering the validation rules for the <see cref="UpdateProductCommand"/> object.
    /// </summary>
    public UpdateProductCommandValidator()
    {
        RuleFor(r => r.Id).NotEmpty().WithMessage("Product Id is required");
        RuleFor(r => r.Name).NotEmpty().WithMessage("Product name is required")
            .Length(2, 150).WithMessage("Name must be between 2 and 150 characters");
        RuleFor(r => r.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
    }
}

/// <summary>
/// Handler for <see cref="UpdateProductCommand"/> that performs the update operation on a product.
/// Implements <see cref="ICommandHandler{UpdateProductCommand, UpdateProductResult}"/> to handle the command with a result.
/// </summary>
internal class UpdateProductCommandHandler(IDocumentSession session) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var productFromDb = await session.LoadAsync<Product>(command.Id, cancellationToken);

        if (productFromDb is null)
        {
            throw new ProductNotFoundException();
        }

        // Update the product with new values.
        productFromDb.Name = command.Name;
        productFromDb.Category = command.Category;
        productFromDb.Description = command.Description;
        productFromDb.ImageFile = command.ImageFile;
        productFromDb.Price = command.Price;

        // Update in the database.
        session.Update(productFromDb);
        await session.SaveChangesAsync(cancellationToken);

        return new UpdateProductResult(true);
    }
}
