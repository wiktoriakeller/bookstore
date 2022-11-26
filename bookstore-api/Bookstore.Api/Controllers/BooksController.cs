using Bookstore.Core.Dtos.Books;
using Bookstore.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Api.Controllers
{
    [ApiController]
    [Route("api/v1/books")]
    public class BooksController : ControllerBase
    {
        private readonly IBooksService _booksService;

        public BooksController(IBooksService booksService)
        {
            _booksService = booksService;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            return Ok(_booksService.GetAllBooks());
        }

        [HttpPost("filters")]
        public IActionResult GetFilteredBooks([FromBody] BooksFiltersDto booksFiltersDto)
        {
            return Ok(_booksService.FilterBooks(booksFiltersDto));
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] AddBookDto addBookDto)
        {
            var bookId = await _booksService.AddBook(addBookDto);
            return Created($"api/vi/books/{bookId}", null);
        }

        [HttpDelete("{bookId}")]
        public async Task<IActionResult> DeleteBook([FromRoute] Guid bookId)
        {
            var deleteBookDto = new DeleteBookDto { Id = bookId };
            await _booksService.DeleteBook(deleteBookDto);
            return Ok();
        }

        [HttpPut("{bookId}")]
        public async Task<IActionResult> UpdateBook([FromBody] UpdateBookDto updateBookDto)
        {
            await _booksService.UpdateBook(updateBookDto);
            return Ok();
        }
    }
}
