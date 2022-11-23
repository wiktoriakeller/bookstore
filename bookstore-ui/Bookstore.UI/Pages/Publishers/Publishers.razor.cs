using Bookstore.Core.Dtos.Publishers;
using Bookstore.UI.ApiInterfaces;
using Bookstore.UI.Common.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace Bookstore.UI.Pages.Publishers
{
    public partial class Publishers : BaseTablePage
    {
        [Inject]
        private IPublishersApi _publishersApi { get; set; }

        private IEnumerable<Publisher> _publishers = Enumerable.Empty<Publisher>();

        private string _publishersNameFilter = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            _publishers = await _publishersApi.GetAllPublishers();
            _isLoading = false;
            StateHasChanged();
        }

        private async Task FilterPublishers(KeyboardEventArgs e)
        {
            if (e.Key == "Enter")
            {
                var filtered = await _publishersApi.GetFilteredPublishers(_publishersNameFilter);
                _publishers = filtered ?? Enumerable.Empty<Publisher>();
                _publishersNameFilter = string.Empty;
                StateHasChanged();
            }
        }

        private async Task OpenAddDialog()
        {
            var parameters = new DialogParameters();
            await ShowDialog<AddPublisher>("Add publisher", parameters, _dialogOptions);
        }

        private async Task OpenUpdateDialog(Publisher publisher)
        {
            var selectedPublisher = new UpdatePublisherDto
            {
                Id = publisher.Id,
                Name = publisher.Name
            };

            var parameters = new DialogParameters
            {
                { "SelectedPublisher", selectedPublisher }
            };

            await ShowDialog<UpdatePublisher>("Edit publisher", parameters, _dialogOptions);
        }

        private async Task OpenDeleteDialog(Publisher publisher)
        {
            var parameters = new DialogParameters
            {
                { "SelectedPublisheriD", publisher.Id }
            };

            await ShowDialog<DeletePublisher>("Delete publisher", parameters, _dialogOptions);
        }

        private async Task ShowDialog<T>(string title, DialogParameters? parameters = null, DialogOptions? options = null)
            where T : ComponentBase
        {
            var dialog = _dialogService.Show<T>(title, parameters, options);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                _publishers = await _publishersApi.GetAllPublishers();
                StateHasChanged();
            }
        }

        private int? GetRowNumber(Publisher element) => _publishers?.TakeWhile(p => p != element).Count();
    }
}
