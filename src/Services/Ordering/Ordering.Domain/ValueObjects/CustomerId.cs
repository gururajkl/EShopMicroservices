﻿namespace Ordering.Domain.ValueObjects;

public record CustomerId
{
    public Guid Value { get; }

    private CustomerId(Guid value) => Value = value;

    private static CustomerId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);

        if (value == Guid.Empty) throw new DomainException("Customer cannot be emoty");

        return new CustomerId(value);
    }
}
