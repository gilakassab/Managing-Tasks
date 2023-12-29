namespace BO;
[Serializable]

public class BlDoesNotExistException : Exception
{
    public BlDoesNotExistException(string? message) : base(message) { }
    public BlDoesNotExistException(string message, Exception innerException)
           : base(message, innerException) { }
}

public class BlNullPropertyException : Exception
{
    public BlNullPropertyException(string? message) : base(message) { }
    public BlNullPropertyException(string message, Exception innerException)
          : base(message, innerException) { }
}

public class BlAlreadyExistsException : Exception
{
    public BlAlreadyExistsException(string? message) : base(message) { }
    public BlAlreadyExistsException(string message, Exception innerException)
              : base(message, innerException) { }
}

public class BlDeletionImpossible : Exception
{
    public BlDeletionImpossible(string? message) : base(message) { }
    public BlDeletionImpossible(string message, Exception innerException)
              : base(message, innerException) { }
}

public class BlXMLFileLoadCreateException : Exception
{
    public BlXMLFileLoadCreateException(string? message) : base(message) { }
}


public class BlFailedToRead : Exception
{
    public BlFailedToReadMilestone(string? message) : base(message) { }
    public BlFailedToReadMilestone(string message, Exception innerException)
              : base(message, innerException) { }
}

public class BlInvalidDataException : Exception
{
    public BlInvalidDataException(string? message) : base(message) { }
    public BlInvalidDataException(string message, Exception innerException)
              : base(message, innerException) { }
}
