namespace BO;

[Serializable]
public class BlDoesNotExistException : Exception
{
    public BlDoesNotExistException(string? message) : base(message) { }
    public BlDoesNotExistException(string message, Exception innerException)
           : base(message, innerException) { }
}

[Serializable]
public class BlNullPropertyException : Exception
{
    public BlNullPropertyException(string? message) : base(message) { }
    public BlNullPropertyException(string message, Exception innerException)
          : base(message, innerException) { }
}

[Serializable]
public class BlAlreadyExistsException : Exception
{
    public BlAlreadyExistsException(string? message) : base(message) { }
    public BlAlreadyExistsException(string message, Exception innerException)
              : base(message, innerException) { }
}

[Serializable]
public class BlDeletionImpossible : Exception
{
    public BlDeletionImpossible(string? message) : base(message) { }
    public BlDeletionImpossible(string message, Exception innerException)
              : base(message, innerException) { }
}

[Serializable]
public class BlXMLFileLoadCreateException : Exception
{
    public BlXMLFileLoadCreateException(string? message) : base(message) { }
}

[Serializable]
public class BlFailedToReadMilestone : Exception
{
    public BlFailedToReadMilestone(string? message) : base(message) { }
    public BlFailedToReadMilestone(string message, Exception innerException)
              : base(message, innerException) { }
}

[Serializable]
public class BlInvalidDataException : Exception
{
    public BlInvalidDataException(string? message) : base(message) { }
    public BlInvalidDataException(string message, Exception innerException)
              : base(message, innerException) { }
}
