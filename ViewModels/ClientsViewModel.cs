using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TouristApp.Models;
using TouristApp.Data;
using TouristApp.Helpers;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace TouristApp.ViewModels
{
    public class ClientsViewModel : BaseViewModel
    {
        private readonly DatabaseService _db;
        public ObservableCollection<Client> Clients { get; }
        public ICommand LoadClientsCommand { get; }
        public ICommand AddClientCommand { get; }
        public ICommand EditClientCommand { get; }
        public ICommand DeleteClientCommand { get; }

        public ClientsViewModel(DatabaseService db)
        {
            _db = db;
            Clients = new ObservableCollection<Client>();
            LoadClientsCommand = new RelayCommand(async _ => await LoadClients());
            AddClientCommand = new RelayCommand(_ => Shell.Current.GoToAsync("/ClientDetail"));
            EditClientCommand = new RelayCommand(async c => await Shell.Current.GoToAsync($"/ClientDetail?clientId={((Client)c).Id}"));
            DeleteClientCommand = new RelayCommand(async c => {
                await _db.DeleteClientAsync((Client)c);
                await LoadClients();
            });
        }

        public async Task LoadClients()
        {
            Clients.Clear();
            var list = await _db.GetClientsAsync();
            foreach (var c in list)
                Clients.Add(c);
        }
    }
}
