namespace Ordering.Domain.Models;

public class Customer : Entity<CustomerId>
{
    public string Name { get; private set; } = default!;
    public string Email { get; private set; } = default!;

    /// <summary>
    /// Create customer entity.
    /// </summary>
    /// <param name="customerId">Customer id.</param>
    /// <param name="name">Name of the customer.</param>
    /// <param name="email">Email of the customer.</param>
    /// <returns>New customer.</returns>
    public static Customer Create(CustomerId customerId, string name, string email)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(email);

        var customer = new Customer()
        {
            Id = customerId,
            Name = name,
            Email = email
        };

        return customer;
    }
}
