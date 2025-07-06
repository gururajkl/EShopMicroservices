namespace Shopping.Web.Pages;
public class IndexModel(ILogger<IndexModel> logger, ICatalogService catalogService) : PageModel
{
    public IEnumerable<ProductModel> ProductList { get; set; } = [];

    public async Task<IActionResult> OnGetAsync()
    {
        logger.LogInformation("Index page loaded.");
        var products = await catalogService.GetProducts();
        ProductList = products.Products;
        return Page();
    }
}
