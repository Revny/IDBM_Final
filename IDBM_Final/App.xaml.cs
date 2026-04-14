using IDBM_Final.Models;
using IDBM_Final.Services;
using IDBM_Final.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;

namespace IDBM_Final
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);

            ServiceProvider = services.BuildServiceProvider();

            var mainViewModel = ServiceProvider.GetRequiredService<MainViewModel>();
            var navService = ServiceProvider.GetRequiredService<INavigationService>() as NavigationService;
            navService.SetMainViewModel(mainViewModel);

            MainWindow mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Width = 1200;
            mainWindow.Height = 900;
            mainWindow.DataContext = mainViewModel;
            mainWindow.Show();
        }
        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<MainWindow>();
            services.AddSingleton<INavigationService, NavigationService>();

            services.AddDbContext<ImdbContext>(options =>
            options.UseSqlServer(ConfigurationManager.ConnectionStrings["IMDBconn"].ConnectionString));
            services.AddSingleton<HomeViewModel>();
            services.AddSingleton<EpisodeViewModel>();
            services.AddSingleton<TitleViewModel>();

            services.AddSingleton<MainViewModel>();

        }
    }
}
