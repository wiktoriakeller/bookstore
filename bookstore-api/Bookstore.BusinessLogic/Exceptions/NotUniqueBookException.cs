namespace Bookstore.BusinessLogic.Exceptions
{
    public class NotUniqueBookException : Exception
    {
        public NotUniqueBookException(string? message) : base(message)
        {
        }
    }
}
