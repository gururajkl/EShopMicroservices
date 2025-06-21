namespace Ordering.Domain.Exceptions;

public class DominException(string message) 
    : Exception($"Domain exception: \"{message}\" throws from Domain layer.") { }

