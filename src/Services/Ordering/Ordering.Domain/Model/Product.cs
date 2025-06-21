namespace Ordering.Domain.Model;

public class Product : Entity<ProductId>
{
    public string Name { get; private set; } = default!;
    public string Price { get; private set; } = default!;
}
