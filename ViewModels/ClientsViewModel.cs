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

        public ICommand LoadClientsCommand { get; }
        public ICommand AddClientCommand { get; }
        public ICommand EditClientCommand { get; }
        public ICommand DeleteClientCommand { get; }

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
        }

        private async Task LoadClientsAsync()
        {
            Clients.Clear();
            var list = await _db.GetClientsAsync();
            foreach (var c in list)
                Clients.Add(c);
        }
    }
}
