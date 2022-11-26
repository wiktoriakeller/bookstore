using Bookstore.UI.ApiInterfaces;
using Microsoft.AspNetCore.Components;

namespace Bookstore.UI.Pages.Books
{
    public partial class DeleteBook : DialogBasePage
    {
        [Parameter]
        public Guid SelectedBookId { get; init; }

        [Inject]
        private IBooksApi _booksApi { get; set; }

        private async Task ConfirmDelete()
        {
            var successMessage = "Deleted book";
            var request = _booksApi.DeleteBook(SelectedBookId);
            await SendRequest(request, successMessage);
        }
    }
}
