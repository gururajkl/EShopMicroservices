using Ordering.Domain.Abstractions;

namespace Ordering.Domain.Model;

public class Customer : Entity<Guid>
{
    public string Name { get; private set; } = default!;
    public string Email { get; private set; } = default!;
}
