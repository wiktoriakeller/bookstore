using Bookstore.Core.Dtos.Publishers;
using Bookstore.UI.ApiInterfaces;
using Bookstore.UI.Common.Validators;
using Microsoft.AspNetCore.Components;
using MudBlazor;

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
                var successMessage = "Added new publisher";
                var request = _publishersApi.AddPublisher(_addPublisher);
                await SendRequest(request, successMessage);
            }
        }
    }
}
