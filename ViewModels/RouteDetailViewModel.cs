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
        public int RouteId
        {
            get => _routeId;
            set
            {
                _routeId = value;
                _ = LoadRoute();
            }
        }

        private Route _route;
        public Route Route
        {
            get => _route;
            set => SetProperty(ref _route, value);
        }
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public RouteDetailViewModel(DatabaseService db)
        {
            _db = db;
            Route = new Route();
            SaveCommand = new RelayCommand(async _ => await Save());
            CancelCommand = new RelayCommand(_ => Shell.Current.GoToAsync(".."));
        }

        private async Task LoadRoute()
        {
            if (RouteId == 0) return;
            Route = await _db.GetRouteAsync(RouteId) ?? new Route();
        }

        private async Task Save()
        {
            await _db.SaveRouteAsync(Route);
            await Shell.Current.GoToAsync("//Routes");
        }
    }
}
