using Microsoft.Maui.Controls;
using TouristApp.Data;
using TouristApp.ViewModels;

namespace TouristApp.Views
{
    public partial class ClientsPage : ContentPage
    {
        public ClientsPage()
        {
            InitializeComponent();

            var dbService = DependencyService.Get<DatabaseService>();

            BindingContext = new ClientsViewModel(dbService);

            ((ClientsViewModel)BindingContext).LoadClientsCommand.Execute(null);
        }
    }
}
