namespace Ordering.Domain.Model;

public class Customer : Entity<ProductId>
{
    public string Name { get; private set; } = default!;
    public string Email { get; private set; } = default!;
}
