using Microsoft.Maui.Controls;
using TouristApp.Data;
using TouristApp.ViewModels;

namespace TouristApp.Views
{
    [QueryProperty(nameof(VoucherId), "voucherId")]
    public partial class VoucherDetailPage : ContentPage
    {
        public int VoucherId { get; set; }

        public VoucherDetailPage()
        {
            InitializeComponent();

            var dbService = DependencyService.Get<DatabaseService>();
            BindingContext = new VoucherDetailViewModel(dbService);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is VoucherDetailViewModel vm)
                vm.VoucherId = VoucherId;
        }
    }
}
