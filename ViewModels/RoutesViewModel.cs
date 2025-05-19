using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TouristApp.Models;
using TouristApp.Data;
using TouristApp.Helpers;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace TouristApp.ViewModels
{
    public class RoutesViewModel : BaseViewModel
    {

        private readonly DatabaseService _db;
        public ObservableCollection<Route> Routes { get; } = new ObservableCollection<Route>();
        public ICommand LoadRoutesCommand { get; }
        public ICommand AddRouteCommand { get; }
        public ICommand EditRouteCommand { get; }
        public ICommand DeleteRouteCommand { get; }

        public RoutesViewModel(DatabaseService db)
        {
            _db = db;
            Routes = new ObservableCollection<Route>();
            LoadRoutesCommand = new RelayCommand(async _ => await LoadRoutes());
            AddRouteCommand = new RelayCommand(_ => Shell.Current.GoToAsync("/RouteDetail"));
            EditRouteCommand = new RelayCommand(async r => await Shell.Current.GoToAsync($"/RouteDetail?routeId={((Route)r).Id}"));
            DeleteRouteCommand = new RelayCommand(async r => {
                await _db.DeleteRouteAsync((Route)r);
                await LoadRoutes();
            });
        }

        public async Task LoadRoutes()
        {
            Routes.Clear();
            var list = await _db.GetRoutesAsync();
            foreach (var r in list)
                Routes.Add(r);
        }
    }
}
