using Microsoft.Maui.Controls;
using TouristApp.Data;
using TouristApp.ViewModels;

namespace TouristApp.Views
{
    [QueryProperty(nameof(ClientId), "clientId")]
    public partial class ClientDetailPage : ContentPage
    {
        public int ClientId { get; set; }

        public ClientDetailPage()
        {
            InitializeComponent();

            var dbService = DependencyService.Get<DatabaseService>();
            BindingContext = new ClientDetailViewModel(dbService);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is ClientDetailViewModel vm)
            {
                vm.ClientId = ClientId;
            }
        }
    }
}
