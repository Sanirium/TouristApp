using Microsoft.Maui.Controls;
using TouristApp.ViewModels;

namespace TouristApp.Views
{
    public partial class RoutesPage : ContentPage
    {
        public RoutesPage(RoutesViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((RoutesViewModel)BindingContext)
                .LoadRoutesCommand
                .Execute(null);
        }
    }
}
