namespace Bookstore.BusinessLogic.Exceptions
{
    public class PublisherHasBooksException : Exception
    {
        public PublisherHasBooksException(string? message) : base(message)
        {
        }
    }
}
