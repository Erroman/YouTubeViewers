using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using YouTubeViewers.WPF.Commands;
using YouTubeViewers.WPF.Models;
using YouTubeViewers.WPF.Stores;

namespace YouTubeViewers.WPF.ViewModels
{
    public class YouTubeViewersListingViewModel:ViewModelBase
    {
        private readonly ObservableCollection<YouTubeViewersListingItemViewModel> _youTubeViewersListingItemViewModels;
        public IEnumerable<YouTubeViewersListingItemViewModel> YouTubeViewersListingItemViewModels => _youTubeViewersListingItemViewModels;

        private readonly SelectedYouTubeViewerStore _selectedYouTubeViewerStore;

        private YouTubeViewersListingItemViewModel _selectedYouTubeViewerListingItemViewModel;

        public YouTubeViewersListingItemViewModel SelectedYouTubeViewerListingItemViewModel
        {
            get { return _selectedYouTubeViewerListingItemViewModel; }
            set 
            { 
                _selectedYouTubeViewerListingItemViewModel = value;
                OnPropertyChanged(nameof(SelectedYouTubeViewerListingItemViewModel));
                _selectedYouTubeViewerStore.SelectedYouTubeViewer = _selectedYouTubeViewerListingItemViewModel?.YouTubeViewer;
            }
        }


        public YouTubeViewersListingViewModel(YouTubeViewersStore youTubeViewersStore, SelectedYouTubeViewerStore selectedYouTubeViewerStore, ModalNavigationStore modalNavigationStore)
        { 
            _selectedYouTubeViewerStore = selectedYouTubeViewerStore;
            _youTubeViewersListingItemViewModels = new ObservableCollection<YouTubeViewersListingItemViewModel>();
            AddYouTubeViewer(new YouTubeViewer("Mary",true,false),modalNavigationStore);
            AddYouTubeViewer(new YouTubeViewer("Sean", false, false), modalNavigationStore);
            AddYouTubeViewer(new YouTubeViewer("Alan", true, true), modalNavigationStore);
            //_youTubeViewersListingItemViewModels.Add(new YouTubeViewersListingItemViewModel(new YouTubeViewer("Mary", true, false)));
            //_youTubeViewersListingItemViewModels.Add(new YouTubeViewersListingItemViewModel(new YouTubeViewer("Sean",true,false)));
            //_youTubeViewersListingItemViewModels.Add(new YouTubeViewersListingItemViewModel(new YouTubeViewer("Alan",true,false)));
        }
        private void AddYouTubeViewer(YouTubeViewer youTubeViewer,ModalNavigationStore modalNavigationStore) 
        {
            ICommand editCommand = new OpenEditYouTubeViewerCommand(youTubeViewer,modalNavigationStore);
            _youTubeViewersListingItemViewModels.Add(new YouTubeViewersListingItemViewModel(youTubeViewer,editCommand));
        }
    }
}
