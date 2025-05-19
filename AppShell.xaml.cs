using TouristApp.Views;

namespace TouristApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(RouteDetailPage), typeof(RouteDetailPage));
            Routing.RegisterRoute(nameof(ClientDetailPage), typeof(ClientDetailPage));
            Routing.RegisterRoute(nameof(VoucherDetailPage), typeof(VoucherDetailPage)
            );
        }
    }
}
