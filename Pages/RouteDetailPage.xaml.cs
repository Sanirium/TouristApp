using Microsoft.Maui.Controls;
using TouristApp.Data;
using TouristApp.ViewModels;

namespace TouristApp.Views
{
    [QueryProperty(nameof(RouteId), "routeId")]
    public partial class RouteDetailPage : ContentPage
    {
        public int RouteId { get; set; }

        public RouteDetailPage()
        {
            InitializeComponent();

            var dbService = DependencyService.Get<DatabaseService>();
            BindingContext = new RouteDetailViewModel(dbService);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is RouteDetailViewModel vm)
            {
                vm.RouteId = RouteId;
            }
        }
    }
}
