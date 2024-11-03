namespace Catalog.API.Products.DeleteProduct;

/// <summary>
/// Command helps to delete the product.
/// </summary>
/// <param name="Id">ID of the product.</param>
public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;

/// <summary>
/// Result pattern for the delete command.
/// </summary>
/// <param name="IsSuccess">Indicates whether the product update operation succeeded.</param>
public record DeleteProductResult(bool IsSuccess);

// Class that class before calling the handler class for validating the Product object.
public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    /// <summary>
    /// Registering the validation rules for the <see cref="DeleteProductCommand"/> object.
    /// </summary>
    public DeleteProductCommandValidator()
    {
        RuleFor(r => r.Id).NotEmpty().WithMessage("Product Id is required");
    }
}

// Handler class for DeleteProductCommand.
internal class DeleteProductCommandHandler(IDocumentSession session) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        session.Delete<Product>(command.Id);
        await session.SaveChangesAsync(cancellationToken);
        return new DeleteProductResult(true);
    }
}
