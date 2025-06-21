namespace Ordering.Domain.Model;

// Building this model as Rich domain model instead of Anemic domain model.
public class Order : Aggregate<OrderId>
{
    private readonly List<OrderItem> _orderItems = new();
    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();

    public CustomerId CustomerId { get; private set; } = default!;
    public OrderName OrderName { get; private set; } = default!;
    public Address ShippingAddress { get; private set; } = default!;
    public Address BillingAddress { get; private set; } = default!;
    public Payment Payment { get; private set; } = default!;
    public OrderStatus Status { get; private set; } = OrderStatus.Pending;
    public decimal TotalPrice
    {
        get => OrderItems.Sum(o => o.Price * o.Quantity);
        private set { }
    }

    // Business logic.
    public static Order Create(OrderId id, CustomerId customerId, OrderName orderName, Address shippingAddress,
        Address billingAddress, Payment payment)
    {
        var order = new Order
        {
            Id = id,
            CustomerId = customerId,
            OrderName = orderName,
            ShippingAddress = shippingAddress,
            BillingAddress = billingAddress,
            Payment = payment,
            Status = OrderStatus.Pending,
        };

        order.AddDomainEvents(new OrderCreatedEvent(order));
        return order;
    }

    public void Update(OrderName orderName, Address shippingAddress,
        Address billingAddress, Payment payment)
    {
        OrderName = orderName;
        ShippingAddress = shippingAddress;
        BillingAddress = billingAddress;
        Payment = payment;
        Status = OrderStatus.Pending;

        AddDomainEvents(new OrderUpdatedEvent(this));
    }

    // Used to add order item.
    public void Add(ProductId productId, int quantity, decimal price)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity, nameof(quantity));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price, nameof(price));

        var orderItem = new OrderItem(Id, productId, quantity, price);

        _orderItems.Add(orderItem);
    }

    // Used to remove the order item.
    public void Remove(ProductId productId)
    {
        var orderItem = _orderItems.FirstOrDefault(o => o.ProductId == productId);

        if (orderItem is not null)
        {
            _orderItems.Remove(orderItem);
        }
    }
}
