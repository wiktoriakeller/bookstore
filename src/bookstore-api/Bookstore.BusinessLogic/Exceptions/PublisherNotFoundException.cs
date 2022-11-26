namespace Bookstore.BusinessLogic.Exceptions
{
    public class PublisherNotFoundException : Exception
    {
        public PublisherNotFoundException(string? message) : base(message)
        {
        }
    }
}
