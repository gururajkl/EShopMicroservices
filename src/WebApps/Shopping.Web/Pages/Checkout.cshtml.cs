namespace Shopping.Web.Pages;

public class CheckoutModel(ILogger<CheckoutModel> logger, IBasketServices basketServices) : PageModel
{
    [BindProperty]
    public BasketCheckoutModel Order { get; set; } = default!;

    public ShoppingCartModel Cart { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync()
    {
        Cart = await basketServices.LoadUserBaset(logger);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        Cart = await basketServices.LoadUserBaset(logger);

        Order.CustomerId = new Guid("D5E88223-8889-4FEA-87F3-D6C7B4C5F7B3");
        Order.UserName = Cart.UserName;
        Order.TotalPrice = Cart.TotalPrice;

        var response = await basketServices.CheckoutBasket(new CheckoutBasketRequest(Order));

        if (response.IsSuccess)
        {
            return RedirectToPage("Confirmation", "OrderSubmitted");
        }

        ModelState.AddModelError(string.Empty, "Checkout failed. Please try again.");
        return Page();
    }
}
