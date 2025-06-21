namespace Ordering.Domain.ValueObjects;

public record class OrderId
{
    public Guid Value { get; }

    private OrderId(Guid value) => Value = value;

    public static OrderId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value, nameof(value));

        if (value == Guid.Empty)
        {
            throw new DominException("OrderId cannot be empty");
        }

        return new OrderId(value);
    }
}
