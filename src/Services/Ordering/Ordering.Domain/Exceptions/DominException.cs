namespace Ordering.Domain.Exceptions;

/// <summary>
/// Custom exception used to throw exception in domain layer.
/// </summary>
/// <param name="message">Exception message.</param>
public class DominException(string message)
    : Exception($"Domain exception: \"{message}\" throws from Domain layer.")
{ }

