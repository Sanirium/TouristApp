using Microsoft.Maui.Controls;
using TouristApp.Data;
using TouristApp.ViewModels;

namespace TouristApp.Views
{
    public partial class VouchersPage : ContentPage
    {
        public VouchersPage()
        {
            InitializeComponent();

            var dbService = DependencyService.Get<DatabaseService>();
            var vm = new VouchersViewModel(dbService);
            BindingContext = vm;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is VouchersViewModel vm)
            {
                vm.LoadVouchersCommand.Execute(null);
            }
        }
    }
}
