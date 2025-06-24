namespace BDTheque.GraphQL.Exceptions;

public class NotFoundIdException(object id) : Exception($"Id \"{id}\" not found");
