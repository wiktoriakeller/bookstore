using Bookstore.Core.Models;
using Bookstore.UI.ApiInterfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace Bookstore.UI.Pages.Publishers
{
    public partial class Publishers
    {
        [Inject]
        private IPublishersApi _publishersApi { get; set; }

        [Inject]
        private IDialogService _dialogService { get; set; }

        private IEnumerable<Publisher> _publishers = Enumerable.Empty<Publisher>();
        private string _publishersNameFilter = string.Empty;
        private bool _isLoading = true;

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
            var options = new DialogOptions
            {
                MaxWidth = MaxWidth.Small,
                FullWidth = true,
                CloseOnEscapeKey = true,
                Position = DialogPosition.Center
            };

            var parameters = new DialogParameters();
            await ShowDialog<AddPublisher>("Add publisher", parameters, options);
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
