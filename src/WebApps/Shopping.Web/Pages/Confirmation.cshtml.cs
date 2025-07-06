namespace Shopping.Web.Pages;

public class ConfirmationModel : PageModel
{
    public string Message { get; set; } = default!;

    public void OnGetContact() 
    {
        Message = "Your email was send.";
    }

    public void OnGetOrderSubmitted() 
    {
        Message = "Your order was submitted successfully.";
    }
}
