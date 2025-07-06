namespace Shopping.Web.Pages;

public class OrderListModel(IOrderingService orderingService, ILogger<OrderListModel> logger) : PageModel
{
    public IEnumerable<OrderModel> Orders { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync()
    {
        logger.LogInformation("Fetching orders list from ordering service.");
        var customerId = new Guid("D5E88223-8889-4FEA-87F3-D6C7B4C5F7B3");
        var response = await orderingService.GetOrdersByCustomer(customerId);
        Orders = response.Orders;
        return Page();
    }
}
