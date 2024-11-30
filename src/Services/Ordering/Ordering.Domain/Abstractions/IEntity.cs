namespace Ordering.Domain.Abstractions;

/// <summary>
/// Defines the abstraction for a domain entity with a generic identifier and tracking properties.
/// </summary>
/// <typeparam name="T">The type of the identifier for the entity.</typeparam>
public interface IEntity<T> : IEntity
{
    public T Id { get; set; }
}

/// <summary>
/// Represents the base interface for domain entities with audit metadata.
/// </summary>
public interface IEntity
{
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
}
