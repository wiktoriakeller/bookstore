using Bookstore.Core.Dtos.Books;
using Bookstore.UI.Common.Models;
using Refit;

namespace Bookstore.UI.ApiInterfaces
{
    public interface IBooksApi
    {
        [Get("/api/v1/books/")]
        Task<IEnumerable<Book>> GetAllBooks();

        [Post("/api/v1/books/filters")]
        Task<IEnumerable<Publisher>> GetFilteredBooks([Body] BooksFiltersDto booksFiltersDto);

        [Post("/api/v1/books/")]
        Task AddBook([Body] AddBookDto addBookDto);

        [Delete("/api/v1/books/{bookId}")]
        Task DeleteBook(Guid bookId);

        [Put("/api/v1/books/{bookId}")]
        Task UpdateBook(Guid bookId, [Body] UpdateBookDto updateBookDto);
    }
}
