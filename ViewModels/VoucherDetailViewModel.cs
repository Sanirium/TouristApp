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
        private int _voucherId;

        public ObservableCollection<Client> AllClients { get; } = new ObservableCollection<Client>();
        public ObservableCollection<Route> AllRoutes { get; } = new ObservableCollection<Route>();

        private Client _selectedClient;
        public Client SelectedClient
        {
            get => _selectedClient;
            set => SetProperty(ref _selectedClient, value);
        }

        private Route _selectedRoute;
        public Route SelectedRoute
        {
            get => _selectedRoute;
            set => SetProperty(ref _selectedRoute, value);
        }

        private Voucher _voucher = new Voucher { DepartureDate = DateTime.Today };
        public Voucher Voucher
        {
            get => _voucher;
            set => SetProperty(ref _voucher, value);
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand DeleteCommand { get; }

        public VoucherDetailViewModel(DatabaseService db)
        {
            _db = db;

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

        private async Task InitializeAsync()
        {
            await LoadLookupsAsync();
            await LoadVoucherAsync();
        }

        private async Task LoadLookupsAsync()
        {
            var clients = await _db.GetClientsAsync();
            AllClients.Clear();
            foreach (var c in clients)
                AllClients.Add(c);

            var routes = await _db.GetRoutesAsync();
            AllRoutes.Clear();
            foreach (var r in routes)
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
                Voucher = await _db.GetVoucherAsync(VoucherId)
                          ?? new Voucher { DepartureDate = DateTime.Today };

                SelectedClient = AllClients.FirstOrDefault(c => c.Id == Voucher.ClientId);
                SelectedRoute = AllRoutes.FirstOrDefault(r => r.Id == Voucher.RouteId);
            }
        }

        private async Task Save()
        {
            if (SelectedClient != null) Voucher.ClientId = SelectedClient.Id;
            if (SelectedRoute != null) Voucher.RouteId = SelectedRoute.Id;

            await _db.SaveVoucherAsync(Voucher);
            await Shell.Current.GoToAsync("..");
        }

        private async Task Delete()
        {
            if (Voucher?.Id > 0)
            {
                await _db.DeleteVoucherAsync(Voucher);
            }
            await Shell.Current.GoToAsync("..");
        }
    }
}
