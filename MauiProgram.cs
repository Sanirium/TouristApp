using Microsoft.Extensions.Logging;
using TouristApp.Data;
using TouristApp.ViewModels;
using TouristApp.Views;

namespace TouristApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts => {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddSingleton<DatabaseService>();

            builder.Services.AddTransient<ClientDetailViewModel>();
            builder.Services.AddTransient<RouteDetailViewModel>();
            builder.Services.AddTransient<RoutesViewModel>();
            builder.Services.AddTransient<VoucherDetailViewModel>();
            builder.Services.AddTransient<ClientsViewModel>();
            builder.Services.AddTransient<VouchersViewModel>();

            builder.Services.AddTransient<ClientDetailPage>();
            builder.Services.AddTransient<RouteDetailPage>();
            builder.Services.AddTransient<RoutesPage>();
            builder.Services.AddTransient<VoucherDetailPage>();
            builder.Services.AddTransient<ClientsPage>();
            builder.Services.AddTransient<VouchersPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
