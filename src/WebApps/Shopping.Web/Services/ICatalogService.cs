namespace Shopping.Web.Services;

public interface ICatalogService
{
    // Annotations using the refit.
    [Get("/catalog-service/products?pageNumber={pageNumber}&pageSize={pageSize}")]
    Task<GetProductsResponse> GetProducts(int? pageNumber = 1, int? pageSize = 10);
    
    [Get("/catalog-service/products/{id}")]
    Task<GetProductByIdResponse> GetProduct(Guid id);
    
    [Get("/catalog-service/products/{category}")]
    Task<GetProductByCategoryResponse> GetProductByCategory(string category);
}
