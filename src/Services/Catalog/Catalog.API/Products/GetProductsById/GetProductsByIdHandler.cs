namespace Catalog.API.Products.GetProductsById;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
public record GetProductByIdResult(Product Product);

internal class GetProductsByIdQueryHandler(IDocumentSession session, ILogger<GetProductsByIdQueryHandler> logger)
    : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation($"GetProductsQueryHandler.Handle called {query}");

        var product = await session.LoadAsync<Product>(query.Id, cancellationToken);

        return product is null ? throw new ProductNotFoundException(query.Id) : new GetProductByIdResult(product);
    }
}
