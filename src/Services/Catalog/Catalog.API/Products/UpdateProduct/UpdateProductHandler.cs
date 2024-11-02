﻿namespace Catalog.API.Products.UpdateProduct;

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