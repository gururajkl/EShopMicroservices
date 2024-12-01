namespace Ordering.Domain.ValueObjects;

public record OrderName
{
    private const int _defaultLength = 5;

    public string Value { get; } = default!;

    private OrderName(string value) => Value = value;

    public static OrderName Of(string value)
    {
        ArgumentNullException.ThrowIfNull(value);
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        ArgumentOutOfRangeException.ThrowIfEqual(value.Length, _defaultLength);

        if (string.IsNullOrEmpty(value)) throw new DomainException("OrderName cannot be empty");

        return new OrderName(value);
    }
}
