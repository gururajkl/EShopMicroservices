namespace Catalog.API.Products.GetProducts;

public record GetProductsQuery() : IQuery<GetProductsResponse>;
public record GetProductsResult(IEnumerable<Product> Products);

internal class GetProductsQueryHandler(IDocumentSession session, ILogger<GetProductsQueryHandler> logger)
    : IQueryHandler<GetProductsQuery, GetProductsResponse>
{
    public async Task<GetProductsResponse> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation($"GetProductsQueryHandler.Handle called {query}");
        var products = await session.Query<Product>().ToListAsync(cancellationToken);
        return new(products);
    }
}
