namespace Ordering.Domain.ValueObjects;

public record OrderItemId
{
    public Guid Value { get; }

    internal static OrderItemId Of(Guid guid)
    {
        throw new NotImplementedException();
    }
}
