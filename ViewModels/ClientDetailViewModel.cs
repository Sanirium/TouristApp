using System.Threading.Tasks;
using TouristApp.Models;
using TouristApp.Data;
using TouristApp.Helpers;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace TouristApp.ViewModels
{
    [QueryProperty(nameof(ClientId), "clientId")]
    public class ClientDetailViewModel : BaseViewModel
    {
        private readonly DatabaseService _db;

        private int _clientId;
        public int ClientId
        {
            get => _clientId;
            set
            {
                _clientId = value;
                _ = LoadClient();
            }
        }

        private Client _client;
        public Client Client
        {
            get => _client;
            set => SetProperty(ref _client, value);
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public ClientDetailViewModel(DatabaseService db)
        {
            _db = db;
            Client = new Client();
            SaveCommand = new RelayCommand(async _ => await Save());
            CancelCommand = new RelayCommand(async _ => await Shell.Current.GoToAsync(".."));
        }

        private async Task LoadClient()
        {
            if (ClientId == 0) return;
            Client = await _db.GetClientAsync(ClientId) ?? new Client();
        }

        private async Task Save()
        {
            await _db.SaveClientAsync(Client);
            await Shell.Current.GoToAsync("//Clients");
        }
    }
}
