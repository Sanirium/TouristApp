using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using TouristApp.Data;
using TouristApp.Models;
using TouristApp.Views;

namespace TouristApp.ViewModels
{
    public class VouchersViewModel : BaseViewModel
    {
        private readonly DatabaseService _db;

        public ObservableCollection<Voucher> Vouchers { get; } = new ObservableCollection<Voucher>();

        public ICommand LoadVouchersCommand { get; }
        public ICommand AddVoucherCommand { get; }
        public ICommand EditVoucherCommand { get; }
        public ICommand DeleteVoucherCommand { get; }

        public VouchersViewModel(DatabaseService db)
        {
            _db = db;

            LoadVouchersCommand = new Command(async () => await LoadVouchersAsync());

            AddVoucherCommand = new Command(async () =>
                await Shell.Current.GoToAsync(nameof(VoucherDetailPage))
            );

            EditVoucherCommand = new Command<Voucher>(async v =>
                await Shell.Current.GoToAsync(
                    $"{nameof(VoucherDetailPage)}?voucherId={v.Id}"
                )
            );

            DeleteVoucherCommand = new Command<Voucher>(async v =>
            {
                await _db.DeleteVoucherAsync(v);
                await LoadVouchersAsync();
            });
        }

        private async Task LoadVouchersAsync()
        {
            Vouchers.Clear();
            var list = await _db.GetVouchersAsync();
            foreach (var v in list)
            {
                var client = await _db.GetClientAsync(v.ClientId);
                var route = await _db.GetRouteAsync(v.RouteId);
                v.ClientFullName = client?.FullName;
                v.RouteCountry = route?.Country;
                Vouchers.Add(v);
            }
        }
    }
}
