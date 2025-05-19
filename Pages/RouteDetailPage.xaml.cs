using Microsoft.Maui.Controls;
using TouristApp.ViewModels;

namespace TouristApp.Views
{
    [QueryProperty(nameof(RouteId), "routeId")]
    public partial class RouteDetailPage : ContentPage
    {
        public int RouteId
        {
            get => ((RouteDetailViewModel)BindingContext).RouteId;
            set => ((RouteDetailViewModel)BindingContext).RouteId = value;
        }

        public RouteDetailPage(RouteDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
