﻿using YouTubeViewers.Domain.Models;
using YouTubeViewers.WPF.Stores;

namespace YouTubeViewers.WPF.ViewModels
{
    public class YouTubeViewersDetailsViewModel : ViewModelBase
    {
        private readonly SelectedYouTubeViewerStore _selectedYouTubeViewerStore;

        private  YouTubeViewer  SelectedYouTubeViewer => _selectedYouTubeViewerStore.SelectedYouTubeViewer;

        public bool HasSelectedYouTubeViewer => SelectedYouTubeViewer != null;

        public string Username => SelectedYouTubeViewer?.Username ?? "Unknown";

        public string IsSubscribedDisplay => (SelectedYouTubeViewer?.IsSubscribed ?? false) ? "Yes" : "No";

        public string IsMemberDisplay => (SelectedYouTubeViewer?.IsMember ?? false) ? "Yes" : "No";

        public YouTubeViewersDetailsViewModel(SelectedYouTubeViewerStore selectedYouTubeViewerStore)
        {

            _selectedYouTubeViewerStore = selectedYouTubeViewerStore;

            _selectedYouTubeViewerStore.SelectedYouTubeViewerChanged += OnSelectedYouTubeViewerChanged;
        }
        protected override void Dispose() 
        {
            _selectedYouTubeViewerStore.SelectedYouTubeViewerChanged -= OnSelectedYouTubeViewerChanged;
        }
        private void OnSelectedYouTubeViewerChanged()
        {
            OnPropertyChanged(nameof(HasSelectedYouTubeViewer));
            OnPropertyChanged(nameof(Username));
            OnPropertyChanged(nameof(IsSubscribedDisplay));
            OnPropertyChanged(nameof(IsMemberDisplay));
        }
    }
}
