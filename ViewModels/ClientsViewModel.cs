using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TouristApp.Models;
using TouristApp.Data;
using TouristApp.Helpers;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using TouristApp.Views;

namespace TouristApp.ViewModels
{
    public class ClientsViewModel : BaseViewModel
    {
        private readonly DatabaseService _db;

        public ObservableCollection<Client> Clients { get; } = new ObservableCollection<Client>();
        private ObservableCollection<Client> _allClients = new ObservableCollection<Client>();
        public ObservableCollection<Client> FilteredClients { get; } = new ObservableCollection<Client>();

        private string _filterText;
        public string FilterText
        {
            get => _filterText;
            set
            {
                if (SetProperty(ref _filterText, value))
                {
                    ApplyFilter();
                }
            }
        }

        public ICommand LoadClientsCommand { get; }
        public ICommand AddClientCommand { get; }
        public ICommand EditClientCommand { get; }
        public ICommand DeleteClientCommand { get; }
        public ICommand ApplyFilterCommand { get; }
        public bool IsBusy { get; private set; }

        public ClientsViewModel(DatabaseService db)
        {
            _db = db;

            LoadClientsCommand = new Command(async () => await LoadClientsAsync());

            AddClientCommand = new Command(async () =>
                await Shell.Current.GoToAsync(nameof(ClientDetailPage))
            );

            EditClientCommand = new Command<Client>(async client =>
                await Shell.Current.GoToAsync(
                    $"{nameof(ClientDetailPage)}?clientId={client.Id}"
                )
            );

            DeleteClientCommand = new Command<Client>(async client =>
            {
                await _db.DeleteClientAsync(client);
                await LoadClientsAsync();
            });

            ApplyFilterCommand = new Command<string>(text =>
            {
                FilterText = text;
            });
        }

        private async Task LoadClientsAsync()
        {
            IsBusy = true;

            _allClients.Clear();
            var list = await _db.GetClientsAsync();

            foreach (var c in list)
                _allClients.Add(c);

            ApplyFilter();

            IsBusy = false;
        }

        private void ApplyFilter()
        {
            FilteredClients.Clear();

            if (string.IsNullOrWhiteSpace(FilterText))
            {
                foreach (var client in _allClients)
                    FilteredClients.Add(client);
            }
            else
            {
                var lower = FilterText.Trim().ToLower();

                var filtered = _allClients.Where(c =>
                    (!string.IsNullOrEmpty(c.LastName) && c.LastName.ToLower().Contains(lower))
                    || (!string.IsNullOrEmpty(c.FirstName) && c.FirstName.ToLower().Contains(lower))
                    || (!string.IsNullOrEmpty(c.MiddleName) && c.MiddleName.ToLower().Contains(lower))
                    || (!string.IsNullOrEmpty(c.Phone) && c.Phone.ToLower().Contains(lower))
                    || (!string.IsNullOrEmpty(c.Address) && c.Address.ToLower().Contains(lower))
                );

                foreach (var client in filtered)
                    FilteredClients.Add(client);
            }
        }
    }
}
