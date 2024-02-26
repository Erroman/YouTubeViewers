using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouTubeViewers.Domain.Commands;
using YouTubeViewers.Domain.Models;
using YouTubeViewers.Domain.Queries;

namespace YouTubeViewers.WPF.Stores
{
    public class YouTubeViewersStore
    {
        private readonly IGetAllYouTubeVIewersQuery _getAllYouTubeVIewersQuery;
        private readonly ICreateYouTubeViewerCommand _createYouTubeViewerCommand;  
        private readonly IUpdateYouTubeViewerCommand _updateYouTubeViewerCommand;
        private readonly IDeleteYouTubeViewerCommand _deleteYouTubeViewerCommand;   

        public event Action<YouTubeViewer>? YouTubeViewerAdded;
        public event Action<YouTubeViewer>? YouTubeViewerUpdated;

        public YouTubeViewersStore(IGetAllYouTubeVIewersQuery getAllYouTubeVIewersQuery, 
            ICreateYouTubeViewerCommand createYouTubeViewerCommand, 
            IUpdateYouTubeViewerCommand updateYouTubeViewerCommand, 
            IDeleteYouTubeViewerCommand deleteYouTubeViewerCommand)
        {
            _getAllYouTubeVIewersQuery = getAllYouTubeVIewersQuery;
            _createYouTubeViewerCommand = createYouTubeViewerCommand;
            _updateYouTubeViewerCommand = updateYouTubeViewerCommand;
            _deleteYouTubeViewerCommand = deleteYouTubeViewerCommand;
        }

 

        public async Task Add(YouTubeViewer youTubeViewer) 
        {
            await _createYouTubeViewerCommand.Execute(youTubeViewer);

            YouTubeViewerAdded?.Invoke(youTubeViewer);
        }

        public async Task Update(YouTubeViewer youTubeViewer)
        {
            await _updateYouTubeViewerCommand.Execute(youTubeViewer);

            YouTubeViewerUpdated?.Invoke(youTubeViewer);
        }
    }
}
