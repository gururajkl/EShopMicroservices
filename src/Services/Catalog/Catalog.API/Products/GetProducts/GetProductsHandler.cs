namespace Catalog.API.Products.GetProducts;

public record GetProductsQuery() : IQuery<GetProductsResponse>;
public record GetProductsResult(IEnumerable<Product> Products);

internal class GetProductsQueryHandler(IDocumentSession session) : IQueryHandler<GetProductsQuery, GetProductsResponse>
{
    public async Task<GetProductsResponse> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>().ToListAsync(cancellationToken);
        return new(products);
    }
}
