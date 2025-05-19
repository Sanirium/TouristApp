using Microsoft.Maui.Controls;
using TouristApp.ViewModels;

namespace TouristApp.Views
{
    public partial class ClientsPage : ContentPage
    {
        public ClientsPage(ClientsViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((ClientsViewModel)BindingContext)
                .LoadClientsCommand
                .Execute(null);
        }
    }
}
