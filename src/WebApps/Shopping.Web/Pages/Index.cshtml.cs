using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Shopping.Web.Pages;
public class IndexModel(ILogger<IndexModel> logger) : PageModel
{
    public IEnumerable<ProductModel> ProductList { get; set; } = [];

    public void OnGet()
    {

    }
}
