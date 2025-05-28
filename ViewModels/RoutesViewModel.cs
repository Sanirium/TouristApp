using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using TouristApp.Data;
using TouristApp.Models;

namespace TouristApp.ViewModels
{
    public class RoutesViewModel : BaseViewModel
    {
        private readonly DatabaseService _db;

        private ObservableCollection<Route> _allRoutes = new ObservableCollection<Route>();

        public ObservableCollection<Route> FilteredRoutes { get; } = new ObservableCollection<Route>();

        public ObservableCollection<string> CountryList { get; } = new ObservableCollection<string>();
        public ObservableCollection<string> ClimateList { get; } = new ObservableCollection<string>();

        private string _filterCountry;
        public string FilterCountry
        {
            get => _filterCountry;
            set => SetProperty(ref _filterCountry, value);
        }

        private string _filterClimate;
        public string FilterClimate
        {
            get => _filterClimate;
            set => SetProperty(ref _filterClimate, value);
        }

        private string _filterMinDuration;
        public string FilterMinDuration
        {
            get => _filterMinDuration;
            set => SetProperty(ref _filterMinDuration, value);
        }

        private string _filterMaxDuration;
        public string FilterMaxDuration
        {
            get => _filterMaxDuration;
            set => SetProperty(ref _filterMaxDuration, value);
        }

        private string _filterMinPrice;
        public string FilterMinPrice
        {
            get => _filterMinPrice;
            set => SetProperty(ref _filterMinPrice, value);
        }

        private string _filterMaxPrice;
        public string FilterMaxPrice
        {
            get => _filterMaxPrice;
            set => SetProperty(ref _filterMaxPrice, value);
        }

        public ICommand LoadRoutesCommand { get; }
        public ICommand AddRouteCommand { get; }
        public ICommand EditRouteCommand { get; }
        public ICommand DeleteRouteCommand { get; }
        public ICommand ApplyFilterCommand { get; }
        public bool IsBusy { get; private set; }

        public RoutesViewModel(DatabaseService db)
        {
            _db = db;

            LoadRoutesCommand = new Command(async () => await LoadRoutesAsync());
            AddRouteCommand = new Command(async () =>
                await Shell.Current.GoToAsync(nameof(Views.RouteDetailPage))
            );
            EditRouteCommand = new Command<Route>(async r =>
                await Shell.Current.GoToAsync($"{nameof(Views.RouteDetailPage)}?routeId={r.Id}")
            );
            DeleteRouteCommand = new Command<Route>(async r =>
            {
                await _db.DeleteRouteAsync(r);
                await LoadRoutesAsync();
            });

            ApplyFilterCommand = new Command(() => ApplyFilter());
        }

        private async Task LoadRoutesAsync()
        {
            if (IsBusy) return;
            IsBusy = true;

            _allRoutes.Clear();
            var list = await _db.GetRoutesAsync();
            foreach (var r in list)
                _allRoutes.Add(r);

            CountryList.Clear();
            CountryList.Add("Все");
            foreach (var c in _allRoutes.Select(x => x.Country).Distinct().OrderBy(x => x))
                CountryList.Add(c);

            ClimateList.Clear();
            ClimateList.Add("Все");
            foreach (var c in _allRoutes.Select(x => x.Climate).Distinct().OrderBy(x => x))
                ClimateList.Add(c);

            FilterCountry = "Все";
            FilterClimate = "Все";

            ApplyFilter();
            IsBusy = false;
        }

        private void ApplyFilter()
        {
            FilteredRoutes.Clear();

            int.TryParse(FilterMinDuration, out int minDur);
            int.TryParse(FilterMaxDuration, out int maxDur);
            decimal.TryParse(FilterMinPrice, out decimal minPrice);
            decimal.TryParse(FilterMaxPrice, out decimal maxPrice);

            foreach (var r in _allRoutes)
            {
                if (FilterCountry != "Все" && r.Country != FilterCountry)
                    continue;

                if (FilterClimate != "Все" && r.Climate != FilterClimate)
                    continue;

                if (minDur > 0 && r.DurationWeeks < minDur)
                    continue;
                if (maxDur > 0 && r.DurationWeeks > maxDur)
                    continue;

                if (minPrice > 0 && r.Price < minPrice)
                    continue;
                if (maxPrice > 0 && r.Price > maxPrice)
                    continue;

                FilteredRoutes.Add(r);
            }
        }
    }
}
