namespace Shopping.Web.Pages;

public class CartModel(ILogger<CartModel> logger, IBasketServices basketServices) : PageModel
{
    public ShoppingCartModel Cart { get; set; } = new();

    public async Task<IActionResult> OnGetAsync()
    {
        Cart = await basketServices.LoadUserBaset(logger);
        return Page();
    }

    public async Task<IActionResult> OnPostRemoveToCartAsync(Guid productId)
    {
        Cart = await basketServices.LoadUserBaset(logger);
        Cart.Items.RemoveAll(item => item.ProductId == productId);
        await basketServices.StoreBasket(new StoreBasketRequest(Cart));
        return RedirectToPage();
    }
}
