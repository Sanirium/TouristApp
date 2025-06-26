using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using TouristApp.Models;
using TouristApp.Data;
using TouristApp.Helpers;

namespace TouristApp.ViewModels
{
    [QueryProperty(nameof(VoucherId), "voucherId")]
    public class VoucherDetailViewModel : BaseViewModel
    {
        private readonly DatabaseService _db;
        private readonly IDiscountPolicy _discountPolicy;
        private int _voucherId;

        public ObservableCollection<Client> AllClients { get; } = new ObservableCollection<Client>();
        public ObservableCollection<Route> AllRoutes { get; } = new ObservableCollection<Route>();

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand DeleteCommand { get; }

        public VoucherDetailViewModel(DatabaseService db, IDiscountPolicy discountPolicy)
        {
            _db = db;
            _discountPolicy = discountPolicy;

            SaveCommand = new Command(async () => await Save());
            CancelCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
            DeleteCommand = new Command(async () => await Delete());

            _ = InitializeAsync();
        }

        public int VoucherId
        {
            get => _voucherId;
            set
            {
                if (SetProperty(ref _voucherId, value))
                    _ = InitializeAsync();
            }
        }

        private Voucher _voucher = new Voucher { DepartureDate = DateTime.Today };
        public Voucher Voucher
        {
            get => _voucher;
            set
            {
                if (SetProperty(ref _voucher, value))
                {
                    _quantityText = Voucher.Quantity.ToString();
                    _discountText = (Voucher.DiscountPercent * 100).ToString();
                    OnPropertyChanged(nameof(QuantityText));
                    OnPropertyChanged(nameof(DiscountText));
                }
            }
        }

        private Client _selectedClient;
        public Client SelectedClient
        {
            get => _selectedClient;
            set
            {
                if (SetProperty(ref _selectedClient, value) && value != null)
                    Voucher.ClientId = value.Id;
            }
        }

        private Route _selectedRoute;
        public Route SelectedRoute
        {
            get => _selectedRoute;
            set
            {
                if (SetProperty(ref _selectedRoute, value) && value != null)
                {
                    Voucher.RouteId = value.Id;
                    Voucher.RoutePrice = value.Price;
                    OnPropertyChanged(nameof(Voucher.RoutePrice));
                }
            }
        }

        private string _quantityText;
        public string QuantityText
        {
            get => _quantityText;
            set
            {
                if (SetProperty(ref _quantityText, value))
                {
                    if (int.TryParse(value, out var q) && q >= 0)
                    {
                        Voucher.Quantity = q;
                        Voucher.DiscountPercent = _discountPolicy.GetDiscountPercent(q);
                        _discountText = (Voucher.DiscountPercent * 100).ToString("0");
                        OnPropertyChanged(nameof(DiscountText));
                    }
                }
            }
        }

        private string _discountText;
        public string DiscountText
        {
            get => _discountText;
            set
            {
                if (SetProperty(ref _discountText, value))
                {
                    if (decimal.TryParse(value, out var pct100) && pct100 >= 0 && pct100 <= 100)
                    {
                        Voucher.DiscountPercent = pct100 / 100m;
                    }
                }
            }
        }

        private async Task InitializeAsync()
        {
            await LoadLookupsAsync();
            await LoadVoucherAsync();
        }

        private async Task LoadLookupsAsync()
        {
            var clients = await _db.GetClientsAsync();
            AllClients.Clear();
            foreach (var c in clients.OrderBy(x => x.FullName))
                AllClients.Add(c);

            var routes = await _db.GetRoutesAsync();
            AllRoutes.Clear();
            foreach (var r in routes.OrderBy(x => x.DisplayName))
                AllRoutes.Add(r);
        }

        private async Task LoadVoucherAsync()
        {
            if (VoucherId == 0)
            {
                Voucher = new Voucher { DepartureDate = DateTime.Today };
            }
            else
            {
                var v = await _db.GetVoucherAsync(VoucherId)
                        ?? new Voucher { DepartureDate = DateTime.Today };
                Voucher = v;
                SelectedClient = AllClients.FirstOrDefault(c => c.Id == v.ClientId);
                SelectedRoute = AllRoutes.FirstOrDefault(r => r.Id == v.RouteId);
            }
        }

        private async Task Save()
        {
            await _db.SaveVoucherAsync(Voucher);
            await Shell.Current.GoToAsync("..");
        }

        private async Task Delete()
        {
            if (Voucher?.Id > 0)
                await _db.DeleteVoucherAsync(Voucher);
            await Shell.Current.GoToAsync("..");
        }
    }
}
