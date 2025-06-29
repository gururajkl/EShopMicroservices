using BuildingBlocks.Exceptions;

namespace Ordering.Application.Exceptions;

public class OrderNotFoundException(Guid Id) : NotFoundException("Order", Id) { }
