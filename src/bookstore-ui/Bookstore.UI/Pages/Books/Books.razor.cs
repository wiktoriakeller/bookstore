using Bookstore.Core.Dtos.Books;
using Bookstore.UI.ApiInterfaces;
using Bookstore.UI.Common.Models;
using Bookstore.UI.Pages.Publishers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace Bookstore.UI.Pages.Books
{
    public partial class Books : BaseTablePage
    {
        [Inject]
        private IBooksApi _booksApi { get; set; }

        [Inject]
        private IPublishersApi _publishersApi { get; set; }

        private IEnumerable<Book> _books = Enumerable.Empty<Book>();

        private IEnumerable<Publisher> _allPublishers = Enumerable.Empty<Publisher>();

        private string _booksTitleFilter = string.Empty;

        private DateRange _dateRange;

        protected override async Task OnInitializedAsync()
        {
            _books = await _booksApi.GetAllBooks();
            _allPublishers = await _publishersApi.GetAllPublishers();
            _isLoading = false;
            _dateRange = new DateRange(DateTime.Now.AddMonths(-2).Date, DateTime.Now.Date);
            StateHasChanged();
        }

        private async Task FilterBooks(KeyboardEventArgs e)
        {
            if (e.Key == "Enter")
            {
                await Filter();
            }
        }

        private async Task Filter()
        {
            var filters = new BooksFiltersDto
            {
                PublishDateStart = _dateRange.Start ?? DateTime.Now.Date,
                PublishDateEnd = _dateRange.End ?? DateTime.Now.Date,
                TitleFilter = _booksTitleFilter
            };

            var filtered = await _booksApi.GetFilteredBooks(filters);
            _books = filtered ?? Enumerable.Empty<Book>();
            _booksTitleFilter = string.Empty;
            StateHasChanged();
        }

        private async Task OpenAddDialog()
        {
            var parameters = new DialogParameters
            {
                { "AllPublishers", _allPublishers }
            };

            await ShowDialog<AddBook>("Add book", parameters, _dialogOptions);
        }

        private async Task OpenUpdateDialog(Book book)
        {
            var selectedBook = new UpdateBookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Genre = book.Genre,
                Reception = (int)book.Reception,
                PublishDate = book.PublishDate,
                PublisherId = book.PublisherId
            };

            var parameters = new DialogParameters
            {
                { "SelectedBook", selectedBook },
                { "AllPublishers", _allPublishers }
            };

            await ShowDialog<UpdateBook>("Edit book", parameters, _dialogOptions);
        }

        private async Task OpenDeleteDialog(Book book)
        {
            var parameters = new DialogParameters
            {
                { "SelectedBookId", book.Id }
            };

            await ShowDialog<DeleteBook>("Delete book", parameters, _dialogOptions);
        }

        private async Task ShowDialog<T>(string title, DialogParameters? parameters = null, DialogOptions? options = null)
            where T : ComponentBase
        {
            var dialog = _dialogService.Show<T>(title, parameters, options);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                _books = await _booksApi.GetAllBooks();
                StateHasChanged();
            }
        }

        private int? GetRowNumber(Book element) => _books?.TakeWhile(p => p != element).Count();
    }
}
