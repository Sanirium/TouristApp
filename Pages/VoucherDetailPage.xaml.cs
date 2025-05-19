using Microsoft.Maui.Controls;
using TouristApp.ViewModels;

namespace TouristApp.Views
{
    [QueryProperty(nameof(VoucherId), "voucherId")]
    public partial class VoucherDetailPage : ContentPage
    {
        public int VoucherId
        {
            get => ((VoucherDetailViewModel)BindingContext).VoucherId;
            set => ((VoucherDetailViewModel)BindingContext).VoucherId = value;
        }

        public VoucherDetailPage(VoucherDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
