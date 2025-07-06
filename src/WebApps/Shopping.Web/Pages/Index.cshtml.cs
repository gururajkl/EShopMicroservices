namespace Shopping.Web.Pages;

public class IndexModel(ILogger<IndexModel> logger, ICatalogService catalogService,
    IBasketServices basketServices) : PageModel
{
    public IEnumerable<ProductModel> ProductList { get; set; } = [];

    public async Task<IActionResult> OnGetAsync()
    {
        logger.LogInformation("Index page loaded.");
        var products = await catalogService.GetProducts();
        ProductList = products.Products;
        return Page();
    }

    public async Task<IActionResult> OnPostAddToCartAsync(Guid productId)
    {
        logger.LogInformation("Add to cart button clicked.");
        var productRespone = await catalogService.GetProduct(productId);
        var basket = await basketServices.LoadUserBaset(logger);

        basket.Items.Add(new ShoppingCartItemModel
        {
            ProductId = productId,
            ProductName = productRespone.Product.Name,
            Price = productRespone.Product.Price,
            Quantity = 1,
            Color = "Black"
        });

        await basketServices.StoreBasket(new StoreBasketRequest(basket));

        return RedirectToPage("Cart");
    }
}
