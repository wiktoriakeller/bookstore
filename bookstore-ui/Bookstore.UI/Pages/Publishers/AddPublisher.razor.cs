using Bookstore.Core.Dtos.Publishers;
using Bookstore.UI.ApiInterfaces;
using Bookstore.UI.Common.Models;
using Bookstore.UI.Common.Validators;
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

        [Inject]
        private IFormValidator<AddPublisherDto> _validator { get; set; }

        private AddPublisherDto _addPublisher = new();
        private MudForm _form;

        private async Task Submit()
        {
            await _form.Validate();

            if (_form.IsValid)
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
        }
    }
}
