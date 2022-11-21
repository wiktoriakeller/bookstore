using Bookstore.Core.Dtos.Books;

namespace Bookstore.Interfaces.Services
{
    public interface IBooksService
    {
        IEnumerable<BookDto> GetAllBooks();

        Task AddBook(AddBookDto addBookDto);

        Task UpdateBook(UpdateBookDto updateBookDto);

        Task DeleteBook(DeleteBookDto deleteBookDto);
    }
}
