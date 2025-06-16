namespace Basket.API.Data;

/// <summary>
/// Contract to do Basket CRUD operation.
/// </summary>
public interface IBasketRepository
{
    Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default);
    Task<ShoppingCart> StoreBasket(ShoppingCart shoppingCart, CancellationToken cancellationToken = default);
    Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default);
}
