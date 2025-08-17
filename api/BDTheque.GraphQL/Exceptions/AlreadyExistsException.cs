namespace BDTheque.GraphQL.Exceptions;

public class AlreadyExistsException(string? message = null) : Exception(message);
