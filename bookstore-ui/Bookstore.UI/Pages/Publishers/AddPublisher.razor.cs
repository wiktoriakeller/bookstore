using Bookstore.Core.Dtos.Publishers;
using Bookstore.UI.ApiInterfaces;
using Bookstore.UI.Common.Dtos;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Refit;
using System.Text.Json;

namespace Bookstore.UI.Pages.Publishers
{
    public partial class AddPublisher : DialogBasePage
    {
        [Inject]
        private IPublishersApi _publishersApi { get; set; }

        private AddPublisherDto _addPublisher = new();
        private MudForm _form;

        private async Task Submit()
        {
            bool success;
            var message = "Added new publisher";

            try
            {
                await _publishersApi.AddPublisher(_addPublisher);
                success = true;
            }
            catch (ApiException ex)
            {
                message = JsonSerializer.Deserialize<ErrorMessage>(ex.Content).Errors;
                success = false;
            }

            AddSnackbarMessage(success, message);
        }

        private void Cancel() => _mudDialog.Cancel();
    }
}
