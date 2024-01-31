using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using YouTubeViewers.WPF.Models;

namespace YouTubeViewers.WPF.ViewModels
{
    public class YouTubeViewersListingItemViewModel:ViewModelBase
    {
        public YouTubeViewer YouTubeViewer;

        public string Username => YouTubeViewer.Username;
        public string IsSubscribedDisplay => YouTubeViewer.IsSubscribed ? "Yes" : "No";
        public string IsMemberDisplay => YouTubeViewer.IsMemeber ? "Yes" : "No";

        public ICommand EditCommand { get; }

        public ICommand DeleteCommand { get; }
        

        public YouTubeViewersListingItemViewModel(YouTubeViewer youTubeViewer)
        {
            YouTubeViewer = youTubeViewer;
        }
    }
}
