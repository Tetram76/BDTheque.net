namespace BDTheque.Data.Exceptions;

public class ValidationException(string? errorMessage) : Exception(errorMessage);
