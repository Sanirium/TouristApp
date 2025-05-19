
using Microsoft.Maui.Controls;
using TouristApp.ViewModels;

namespace TouristApp.Views
{
    public partial class VouchersPage : ContentPage
    {
        public VouchersPage(VouchersViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((VouchersViewModel)BindingContext)
                .LoadVouchersCommand
                .Execute(null);
        }
    }
}
