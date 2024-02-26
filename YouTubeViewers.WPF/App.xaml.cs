using Microsoft.EntityFrameworkCore;
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
        YouTubeViewersDBContextFactory _youTubeViewersDBContextFactory;
        private readonly IGetAllYouTubeVIewersQuery _getAllYouTubeVIewersQuery;
        private readonly ICreateYouTubeViewerCommand _createYouTubeViewerCommand;
        private readonly IUpdateYouTubeViewerCommand _updateYouTubeViewerCommand;
        private readonly IDeleteYouTubeViewerCommand _deleteYouTubeViewerCommand;
        private readonly YouTubeViewersStore _youTubeViewersStore;
        private readonly SelectedYouTubeViewerStore _selectedYouTubeViewerStore;
        public App() 
        { 
            String connectionString = "Data Source=YouTubeViewers.db";
            _modalNavigationStore = new ModalNavigationStore();
            _youTubeViewersDBContextFactory = new YouTubeViewersDBContextFactory(
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
            YouTubeViewersViewModel youTubeViewersViewModel = new YouTubeViewersViewModel(
                _youTubeViewersStore,
                _selectedYouTubeViewerStore, 
                _modalNavigationStore);
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_modalNavigationStore, youTubeViewersViewModel)
            };
            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}
