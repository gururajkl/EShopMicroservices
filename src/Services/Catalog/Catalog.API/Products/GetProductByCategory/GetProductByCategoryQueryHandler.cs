namespace Catalog.API.Products.GetProductByCategory;

public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;
public record GetProductByCategoryResult(IEnumerable<Product> Products);

internal class GetProductByCategoryQueryHandler(IDocumentSession session, ILogger<GetProductByCategoryQueryHandler> logger)
    : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation($"GetProductsByCategoryQueryHandler.Handle called {query}");

        var products = await session.Query<Product>().Where(s => s.Category.Contains(query.Category)).ToListAsync(cancellationToken);

        return products is null ? throw new ProductNotFoundException() : new GetProductByCategoryResult(products);
    }
}
