namespace Basket.API.Data;

/// <summary>
/// Represents a repository interface for managing shopping cart data in the Basket API.
/// Provides methods to retrieve, store, and delete a user's shopping cart.
/// </summary>
public interface IBasketRepository
{
    /// <summary>
    /// Retrieves the shopping cart for the specified user.
    /// </summary>
    /// <param name="userName">The username associated with the shopping cart.</param>
    /// <param name="cancellationToken">Token for request cancellation.</param>
    /// <returns>The user's shopping cart as a <see cref="ShoppingCart"/> object.</returns>
    Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default);

    /// <summary>
    /// Stores or updates the shopping cart for the specified user.
    /// </summary>
    /// <param name="basket">The <see cref="ShoppingCart"/> object to store or update.</param>
    /// <param name="cancellationToken">Token for request cancellation.</param>
    /// <returns>The stored or updated <see cref="ShoppingCart"/> object.</returns>
    Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes the shopping cart for the specified user.
    /// </summary>
    /// <param name="userName">The username associated with the shopping cart.</param>
    /// <param name="cancellationToken">Token for request cancellation.</param>
    /// <returns>A boolean indicating if the deletion was successful.</returns>
    Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default);
}
