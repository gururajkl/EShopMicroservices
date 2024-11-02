namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    : ICommand<UpdateProductResult>;
public record UpdateProductResult(bool IsSuccess);

internal class UpdateProductCommandHandler(IDocumentSession session) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var productFromDb = await session.LoadAsync<Product>(command.Id, cancellationToken);

        if (productFromDb is null)
        {
            throw new ProductNotFoundException();
        }

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
