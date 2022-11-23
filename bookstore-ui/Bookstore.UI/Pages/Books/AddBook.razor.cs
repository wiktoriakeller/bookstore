using Bookstore.Core.Dtos.Books;
using Bookstore.UI.ApiInterfaces;
using Bookstore.UI.Common;
using Bookstore.UI.Common.Models;
using Bookstore.UI.Common.Validators;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Bookstore.UI.Pages.Books
{
    public partial class AddBook
    {
        [Parameter]
        public IEnumerable<Publisher> AllPublishers { get; init; }

        [Inject]
        private IBooksApi _booksApi { get; set; }

        [Inject]
        private IFormValidator<AddBookDto> _validator { get; set; }

        private AddBookDto _addBook = new();

        private Reception _selectedReception = Reception.Bad;

        private Publisher _selectedPublisher = new();

        private MudForm _form;

        private async Task Submit()
        {
            await _form.Validate();

            if (_form.IsValid)
            {
                _addBook.PublisherId = _selectedPublisher.Id;
                _addBook.Reception = (int)_selectedReception;
                var successMessage = "Added new book";
                var request = _booksApi.AddBook(_addBook);
                await SendRequest(request, successMessage);
            }
        }
    }
}
