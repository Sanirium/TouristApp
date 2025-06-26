using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using TouristApp.Data;
using TouristApp.Models;

namespace TouristApp.ViewModels
{
    public class VouchersViewModel : BaseViewModel
    {
        private readonly DatabaseService _db;

        private ObservableCollection<Voucher> _allVouchers = new ObservableCollection<Voucher>();
        public ObservableCollection<Voucher> FilteredVouchers { get; } = new ObservableCollection<Voucher>();

        public ObservableCollection<string> ClientList { get; } = new ObservableCollection<string>();
        public ObservableCollection<string> RouteList { get; } = new ObservableCollection<string>();

        public string FilterClient { get; set; }
        public string FilterRoute { get; set; }

        public DateTime FilterFromDate { get; set; } = DateTime.Today.AddMonths(-1);
        public DateTime FilterToDate { get; set; } = DateTime.Today.AddMonths(1);

        public string FilterMinDiscount { get; set; }
        public string FilterMaxDiscount { get; set; }
        public string FilterMinQuantity { get; set; }
        public string FilterMaxQuantity { get; set; }

        public ICommand LoadVouchersCommand { get; }
        public ICommand AddVoucherCommand { get; }
        public ICommand EditVoucherCommand { get; }
        public ICommand DeleteVoucherCommand { get; }
        public ICommand ApplyFilterCommand { get; }
        public bool IsBusy { get; private set; }

        public VouchersViewModel(DatabaseService db)
        {
            _db = db;

            LoadVouchersCommand = new Command(async () => await LoadAsync());
            AddVoucherCommand = new Command(async () =>
                await Shell.Current.GoToAsync(nameof(Views.VoucherDetailPage))
            );
            EditVoucherCommand = new Command<Voucher>(async v =>
                await Shell.Current.GoToAsync($"{nameof(Views.VoucherDetailPage)}?voucherId={v.Id}")
            );
            DeleteVoucherCommand = new Command<Voucher>(async v =>
            {
                await _db.DeleteVoucherAsync(v);
                await LoadAsync();
            });
            ApplyFilterCommand = new Command(ApplyFilter);
        }

        private async Task LoadAsync()
        {
            if (IsBusy) return;
            IsBusy = true;

            ClientList.Clear();
            ClientList.Add("Все");
            var clients = await _db.GetClientsAsync();
            foreach (var c in clients.OrderBy(x => x.FullName))
                ClientList.Add(c.FullName);

            RouteList.Clear();
            RouteList.Add("Все");
            var routes = await _db.GetRoutesAsync();
            foreach (var r in routes.OrderBy(x => x.DisplayName))
                RouteList.Add(r.DisplayName);

            FilterClient = "Все";
            FilterRoute = "Все";

            _allVouchers.Clear();
            var list = await _db.GetVouchersAsync();
            foreach (var v in list)
            {
                var client = await _db.GetClientAsync(v.ClientId);
                var route = await _db.GetRouteAsync(v.RouteId);

                v.ClientFullName = client?.FullName ?? "";
                v.RouteDisplayName = route?.DisplayName ?? "";
                v.RoutePrice = route?.Price ?? 0m;

                _allVouchers.Add(v);
            }

            ApplyFilter();
            IsBusy = false;
        }

        private void ApplyFilter()
        {
            FilteredVouchers.Clear();

            int.TryParse(FilterMinDiscount, out int minDisc);
            int.TryParse(FilterMaxDiscount, out int maxDisc);
            int.TryParse(FilterMinQuantity, out int minQty);
            int.TryParse(FilterMaxQuantity, out int maxQty);

            foreach (var v in _allVouchers)
            {
                if (FilterClient != "Все" && v.ClientFullName != FilterClient)
                    continue;

                if (FilterRoute != "Все" && v.RouteDisplayName != FilterRoute)
                    continue;

                if (v.DepartureDate < FilterFromDate || v.DepartureDate > FilterToDate)
                    continue;

                var discountPct = v.DiscountPercent * 100;
                if (minDisc > 0 && discountPct < minDisc)
                    continue;
                if (maxDisc > 0 && discountPct > maxDisc)
                    continue;

                if (minQty > 0 && v.Quantity < minQty)
                    continue;
                if (maxQty > 0 && v.Quantity > maxQty)
                    continue;

                FilteredVouchers.Add(v);
            }
        }
    }
}
