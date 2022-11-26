using Bookstore.Core.Dtos.Books;
using Bookstore.UI.ApiInterfaces;
using Bookstore.UI.Common;
using Bookstore.UI.Common.Models;
using Bookstore.UI.Common.Validators;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Bookstore.UI.Pages.Books
{
    public partial class UpdateBook : DialogBasePage
    {
        [Parameter]
        public IEnumerable<Publisher> AllPublishers { get; init; }

        [Parameter]
        public UpdateBookDto SelectedBook { get; set; }

        [Inject]
        private IBooksApi _booksApi { get; set; }

        [Inject]
        private IFormValidator<UpdateBookDto> _validator { get; set; }

        private Reception _selectedReception;

        private Publisher _selectedPublisher;

        private DateTime? _selectedDate = DateTime.Now;

        private MudForm _form;

        protected override void OnInitialized()
        {
            _selectedReception = (Reception)SelectedBook.Reception;
            _selectedPublisher = AllPublishers.First(p => p.Id == SelectedBook.PublisherId);
            _selectedDate = SelectedBook.PublishDate;
        }

        private async Task Submit()
        {
            await _form.Validate();

            if (_form.IsValid)
            {
                SelectedBook.PublisherId = _selectedPublisher.Id;
                SelectedBook.Reception = (int)_selectedReception;
                SelectedBook.PublishDate = (DateTime)_selectedDate!;
                var successMessage = "Updated book";
                var request = _booksApi.UpdateBook(SelectedBook.Id, SelectedBook);
                await SendRequest(request, successMessage);
            }
        }
    }
}
