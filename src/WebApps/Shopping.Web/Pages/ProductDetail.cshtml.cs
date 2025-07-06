namespace Shopping.Web.Pages;

public class ProductDetailModel(ILogger<ProductDetailModel> logger, IBasketServices basketServices,
    ICatalogService catalogService) : PageModel
{
    public ProductModel Product { get; set; } = default!;

    [BindProperty]
    public string Color { get; set; } = default!;

    [BindProperty]
    public int Quantity { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(Guid productId)
    {
        var response = await catalogService.GetProduct(productId);
        Product = response.Product;
        return Page();
    }

    public async Task<IActionResult> OnPostAddToCartAsync(Guid productId)
    {
        var productResponse = await catalogService.GetProduct(productId);
        var basket = await basketServices.LoadUserBaset(logger);

        basket.Items.Add(new ShoppingCartItemModel
        {
            ProductId = productId,
            ProductName = productResponse.Product.Name,
            Price = productResponse.Product.Price,
            Quantity = 1,
            Color = "Black"
        });

        await basketServices.StoreBasket(new StoreBasketRequest(basket));
        return RedirectToPage("Cart");
    }
}
