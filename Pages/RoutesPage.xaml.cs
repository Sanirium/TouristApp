using Microsoft.Maui.Controls;
using TouristApp.Data;
using TouristApp.ViewModels;

namespace TouristApp.Views
{
    public partial class RoutesPage : ContentPage
    {
        public RoutesPage()
        {
            InitializeComponent();

            var dbService = DependencyService.Get<DatabaseService>();
            var vm = new RoutesViewModel(dbService);
            BindingContext = vm;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is RoutesViewModel vm)
                vm.LoadRoutesCommand.Execute(null);
        }
    }
}
