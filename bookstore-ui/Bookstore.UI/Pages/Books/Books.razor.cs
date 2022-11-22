using Bookstore.Core.Dtos.Books;
using Bookstore.UI.ApiInterfaces;
using Bookstore.UI.Common.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace Bookstore.UI.Pages.Books
{
    public partial class Books
    {
        [Inject]
        private IBooksApi _booksApi { get; set; }

        [Inject]
        private IPublishersApi _publishersApi { get; set; }

        [Inject]
        private IDialogService _dialogService { get; set; }

        private IEnumerable<Book> _books = Enumerable.Empty<Book>();

        private IEnumerable<Publisher> _allPublishers = Enumerable.Empty<Publisher>();

        private bool _isLoading = true;

        private DialogOptions _dialogOptions = new DialogOptions
        {
            MaxWidth = MaxWidth.Small,
            FullWidth = true,
            CloseOnEscapeKey = true,
            Position = DialogPosition.Center
        };

        protected override async Task OnInitializedAsync()
        {
            _books = await _booksApi.GetAllBooks();
            _allPublishers = await _publishersApi.GetAllPublishers();
            _isLoading = false;
            StateHasChanged();
        }

        private async Task FilterBooks(KeyboardEventArgs e)
        {
            if (e.Key == "Enter")
            {
                StateHasChanged();
            }
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
                Reception = book.Reception,
                PublisherId = book.PublisherId,
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
