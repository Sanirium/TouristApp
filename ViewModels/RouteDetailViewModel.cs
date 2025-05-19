using System;
using System.Threading.Tasks;
using TouristApp.Models;
using TouristApp.Data;
using TouristApp.Helpers;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace TouristApp.ViewModels
{

    [QueryProperty(nameof(RouteId), "routeId")]
    public class RouteDetailViewModel : BaseViewModel
    {
        private readonly DatabaseService _db;
        private int _routeId;

        private Route _route;
        public Route Route
        {
            get => _route;
            set => SetProperty(ref _route, value);
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand DeleteCommand { get; }

        public RouteDetailViewModel(DatabaseService db)
        {
            _db = db;
            Route = new Route();

            SaveCommand = new Command(async () => await Save());
            CancelCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
            DeleteCommand = new Command(async () => await Delete());
        }

        public int RouteId
        {
            get => _routeId;
            set
            {
                if (SetProperty(ref _routeId, value))
                {
                    LoadRoute();
                }
            }
        }

        private async Task LoadRoute()
        {
            if (RouteId == 0)
            {
                Route = new Route();
            }
            else
            {
                Route = await _db.GetRouteAsync(RouteId) ?? new Route();
            }
        }

        private async Task Save()
        {
            await _db.SaveRouteAsync(Route);
            await Shell.Current.GoToAsync("..");
        }

        private async Task Delete()
        {
            if (Route?.Id > 0)
            {
                await _db.DeleteRouteAsync(Route);
            }
            await Shell.Current.GoToAsync("..");
        }
    }
}
