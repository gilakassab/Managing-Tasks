
namespace BO;
internal class Exceptions
{
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
    }
    public class BlAlreadyExistsException : Exception {
        public BlAlreadyExistsException(string? message) : base(message) { }
    }
    public class BlDeletionImpossible : Exception
    {
        public BlDeletionImpossible(string? message) : base(message) { }
    }
    public class BlXMLFileLoadCreateException : Exception
    {
        public BlXMLFileLoadCreateException(string? message) : base(message) { }
    }
}
