namespace Ordering.Application.Orders.Commands.DeleteOrder;

public class DeleteOrderHandler(IApplicationDbContext dbContext) : ICommandHandler<DeletOrderCommand, DeleteOrderResult>
{
    public async Task<DeleteOrderResult> Handle(DeletOrderCommand command, CancellationToken cancellationToken)
    {
        // Get the order from DB.
        OrderId orderId = OrderId.Of(command.OrderId);
        var order = await dbContext.Orders.FindAsync([orderId], cancellationToken);

        if (order is null)
        {
            throw new OrderNotFoundException(command.OrderId);
        }

        // Delete the order.
        dbContext.Orders.Remove(order);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new DeleteOrderResult(true);
    }
}
