using Bookstore.UI.ApiInterfaces;
using Microsoft.AspNetCore.Components;

namespace Bookstore.UI.Pages.Publishers
{
    public partial class DeletePublisher : DialogBasePage
    {
        [Parameter]
        public Guid SelectedPublisherId { get; init; }

        [Inject]
        private IPublishersApi _publishersApi { get; set; }

        private async Task ConfirmDelete()
        {
            var successMessage = "Deleted publisher";
            var request = _publishersApi.DeletePublisher(SelectedPublisherId);
            await SendRequest(request, successMessage);
        }
    }
}
