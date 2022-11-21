namespace Bookstore.BusinessLogic.Exceptions
{
    public class NotUniquePublisherException : Exception
    {
        public NotUniquePublisherException(string? message) : base(message)
        {
        }
    }
}
