using Bookstore.Core.Dtos.Publishers;
using Bookstore.UI.ApiInterfaces;
using Bookstore.UI.Common.Validators;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Bookstore.UI.Pages.Publishers
{
    public partial class UpdatePublisher : DialogBasePage
    {
        [Parameter]
        public UpdatePublisherDto SelectedPublisher { get; set; }

        [Inject]
        private IPublishersApi _publishersApi { get; set; }

        [Inject]
        private IFormValidator<UpdatePublisherDto> _validator { get; set; }

        private MudForm _form;

        private async Task Submit()
        {
            await _form.Validate();

            if (_form.IsValid)
            {
                var successMessage = "Updated publisher";
                var request = _publishersApi.UpdatePublisher(SelectedPublisher.Id, SelectedPublisher);
                await SendRequest(request, successMessage);
            }
        }
    }
}
