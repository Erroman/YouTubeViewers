using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using YouTubeViewers.Domain.Commands;
using YouTubeViewers.Domain.Queries;
using YouTubeViewers.EntityFramework;
using YouTubeViewers.EntityFramework.Commands;
using YouTubeViewers.EntityFramework.Queries;
using YouTubeViewers.WPF.Stores;
using YouTubeViewers.WPF.ViewModels;


namespace YouTubeViewers.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        YouTubeViewersDbContextFactory _youTubeViewersDBContextFactory;
        private readonly IGetAllYouTubeVIewersQuery _getAllYouTubeVIewersQuery;
        private readonly ICreateYouTubeViewerCommand _createYouTubeViewerCommand;
        private readonly IUpdateYouTubeViewerCommand _updateYouTubeViewerCommand;
        private readonly IDeleteYouTubeViewerCommand _deleteYouTubeViewerCommand;
        private readonly YouTubeViewersStore _youTubeViewersStore;
        private readonly SelectedYouTubeViewerStore _selectedYouTubeViewerStore;

        private readonly IHost _host;

        public App() 
        { 
            _host = Host.CreateDefaultBuilder()
                       .ConfigureServices((context, services) =>
                       {
                           string connectionString = "Data Source=YouTubeViewers.db";

                           services.AddSingleton<DbContextOptions>(new DbContextOptionsBuilder().UseSqlite(connectionString).Options);
                           services.AddSingleton<YouTubeViewersDbContextFactory>();

                           services.AddSingleton<IGetAllYouTubeVIewersQuery,GetAllYouTubeViewersQuery>();
                           services.AddSingleton<ICreateYouTubeViewerCommand,CreateYouTubeViewerCommand>();
                           services.AddSingleton<IUpdateYouTubeViewerCommand,UpdateYouTubeViewerCommand>();
                           services.AddSingleton<IDeleteYouTubeViewerCommand,DeleteYouTubeViewerCommand>();

                           services.AddSingleton<ModalNavigationStore>();
                           services.AddSingleton<YouTubeViewersStore>();
                           services.AddSingleton<SelectedYouTubeViewerStore>();

                           services.AddTransient<YouTubeViewersViewModel>(CreateYouTubeViewersViewModel);
                           services.AddSingleton<MainViewModel>();

                           services.AddSingleton<MainWindow>((services) => new MainWindow() 
                           { 
                               DataContext = services.GetRequiredService<MainViewModel>() 
                           });
                       })
                       .Build();    
            String connectionString = "Data Source=YouTubeViewers.db";

            _modalNavigationStore = new ModalNavigationStore();
            _youTubeViewersDBContextFactory = new YouTubeViewersDbContextFactory(
                new DbContextOptionsBuilder().UseSqlite(connectionString).Options);
            _getAllYouTubeVIewersQuery = new GetAllYouTubeViewersQuery(_youTubeViewersDBContextFactory);
            _createYouTubeViewerCommand = new CreateYouTubeViewerCommand(_youTubeViewersDBContextFactory);
            _updateYouTubeViewerCommand = new UpdateYouTubeViewerCommand(_youTubeViewersDBContextFactory);
            _deleteYouTubeViewerCommand = new DeleteYouTubeViewerCommand(_youTubeViewersDBContextFactory);
            _youTubeViewersStore = new YouTubeViewersStore(_getAllYouTubeVIewersQuery,
                _createYouTubeViewerCommand, 
                _updateYouTubeViewerCommand, 
                _deleteYouTubeViewerCommand);
            _selectedYouTubeViewerStore = new SelectedYouTubeViewerStore(_youTubeViewersStore);

        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();
            YouTubeViewersDbContextFactory youTubeViewersDbContextFactory = 
                _host.Services.GetRequiredService<YouTubeViewersDbContextFactory>();
            using (YouTubeViewersDbContext context = youTubeViewersDbContextFactory.Create()) 
            { 
                context.Database.Migrate(); 
            }

            YouTubeViewersViewModel youTubeViewersViewModel = YouTubeViewersViewModel.LoadViewModel(
                _youTubeViewersStore,
                _selectedYouTubeViewerStore, 
                _modalNavigationStore);

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_modalNavigationStore, youTubeViewersViewModel)
            };
            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _host.StopAsync();
            _host.Dispose();

            base.OnExit(e);
        }
        private YouTubeViewersViewModel CreateYouTubeViewersViewModel(IServiceProvider services)
        {
            return YouTubeViewersViewModel.LoadViewModel(
                services.GetRequiredService<YouTubeViewersStore>(), 
                services.GetRequiredService<SelectedYouTubeViewerStore>(), 
                services.GetRequiredService<ModalNavigationStore>());
        }

    }
}
