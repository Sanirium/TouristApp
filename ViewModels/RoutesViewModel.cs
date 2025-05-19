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

            LoadRoutesCommand = new Command(async () => await LoadRoutesAsync());

            AddRouteCommand = new Command(async () =>
                await Shell.Current.GoToAsync(nameof(RouteDetailPage))
            );

            EditRouteCommand = new Command<Route>(async route =>
                await Shell.Current.GoToAsync($"{nameof(RouteDetailPage)}?routeId={route.Id}")
            );

            DeleteRouteCommand = new Command<Route>(async route =>
            {
                await _db.DeleteRouteAsync(route);
                await LoadRoutesAsync();
            });
        }

        private async Task LoadRoutesAsync()
        {
            Routes.Clear();
            var list = await _db.GetRoutesAsync();
            foreach (var r in list)
                Routes.Add(r);
        }
    }
}
