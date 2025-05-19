using Microsoft.Maui.Controls;
using TouristApp.Data;
using TouristApp.ViewModels;

namespace TouristApp.Views
{
    [QueryProperty(nameof(ClientId), "clientId")]
    public partial class ClientDetailPage : ContentPage
    {
        public int ClientId
        {
            get => ((ClientDetailViewModel)BindingContext).ClientId;
            set => ((ClientDetailViewModel)BindingContext).ClientId = value;
        }

        public ClientDetailPage(ClientDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
