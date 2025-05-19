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

        private Client _client;
        public Client Client
        {
            get => _client;
            set => SetProperty(ref _client, value);
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand DeleteCommand { get; }

        public ClientDetailViewModel(DatabaseService db)
        {
            _db = db;
            Client = new Client();

            SaveCommand = new Command(async () => await Save());
            CancelCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
            DeleteCommand = new Command(async () => await Delete());
        }

        public int ClientId
        {
            get => _clientId;
            set
            {
                if (SetProperty(ref _clientId, value))
                {
                    _ = LoadClientAsync();
                }
            }
        }

        private async Task LoadClientAsync()
        {
            if (ClientId == 0)
                Client = new Client();
            else
                Client = await _db.GetClientAsync(ClientId) ?? new Client();
        }

        private async Task Save()
        {
            await _db.SaveClientAsync(Client);
            await Shell.Current.GoToAsync("..");
        }

        private async Task Delete()
        {
            if (Client?.Id > 0)
                await _db.DeleteClientAsync(Client);
            await Shell.Current.GoToAsync("..");
        }
    }
}
