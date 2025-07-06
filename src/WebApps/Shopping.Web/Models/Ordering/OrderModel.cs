﻿namespace Shopping.Web.Models.Ordering;

public record OrderModel(Guid Id, Guid CustomerId, string OrderName,
    AddressModel ShippingAddress, AddressModel BillingAddress,
    PaymentModel Payment, OrderStatus OrderStatus, List<OrderItemModel> OrderItems);

public record AddressModel(string FirstName, string LastName, string EmailAddress, string AddressLine, string Country,
    string State, string ZipCode);

public record PaymentModel(string CardName, string CardNumber, string Expiration, string Cvv, int PaymentMethod);

public record OrderItemModel(Guid OrderId, Guid ProductId, int Quantity, decimal Price);

public enum OrderStatus
{
    Draft = 0,
    Pending = 1,
    Completed = 2,
    Cancelled = 3,
}

// Wrapper class for OrderModel to be used in the API response.
public record GetOrdersResponse(PaginatedResult<OrderModel> Orders);
public record GetOrdersByNameResponse(IEnumerable<OrderModel> Orders);
public record GetOrdersByCustomerResponse(IEnumerable<OrderModel> Orders);