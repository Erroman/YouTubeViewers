using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using YouTubeViewers.WPF.Models;

namespace YouTubeViewers.WPF.Stores
{
    public class SelectedYouTubeViewerStore
    {
		private readonly YouTubeViewersStore _youTubeViewersStore;

        private YouTubeViewer _selectedYouTubeViewer;

		public YouTubeViewer SelectedYouTubeViewer
		{
			get 
			{ 
				return _selectedYouTubeViewer; 
			}
			set 
			{ 
				_selectedYouTubeViewer = value; 
				SelectedYouTubeViewerChanged?.Invoke();
            }
		}
		public event Action SelectedYouTubeViewerChanged;

        public SelectedYouTubeViewerStore(YouTubeViewersStore youTubeViewersStore)
        {
            _youTubeViewersStore = youTubeViewersStore;

            _youTubeViewersStore.YouTubeViewerUpdated += _youTubeViewersStore_YouTubeViewerUpdated;
        }

        private void _youTubeViewersStore_YouTubeViewerUpdated(YouTubeViewer youTubeViewer)
        {
            if(youTubeViewer.Id == SelectedYouTubeViewer?.Id) 
            {
                SelectedYouTubeViewer = youTubeViewer;
            }
        }
    }
}
