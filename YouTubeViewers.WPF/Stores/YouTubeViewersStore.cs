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

        private readonly List<YouTubeViewer> _youTubeViewers;

        public IEnumerable<YouTubeViewer> YouTubeViewers  => _youTubeViewers;

        public event Action? YouTubeViewersLoaded;
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

            _youTubeViewers = new();
        }

        public async Task Load() 
        { 
             IEnumerable<YouTubeViewer> youTubeViewers = await _getAllYouTubeVIewersQuery.Execute();

            _youTubeViewers.Clear();
            _youTubeViewers.AddRange(youTubeViewers);

            YouTubeViewersLoaded?.Invoke();
        }

        public async Task Add(YouTubeViewer youTubeViewer) 
        {
            await _createYouTubeViewerCommand.Execute(youTubeViewer);

            _youTubeViewers.Add(youTubeViewer);

            YouTubeViewerAdded?.Invoke(youTubeViewer);
        }

        public async Task Update(YouTubeViewer youTubeViewer)
        {
            await _updateYouTubeViewerCommand.Execute(youTubeViewer);

            int currentIndex = _youTubeViewers.FindIndex(y => y.Id == youTubeViewer.Id);

            if (currentIndex != -1) 
            { 
                _youTubeViewers[currentIndex] = youTubeViewer; 
            }
            else
            {
                _youTubeViewers.Add(youTubeViewer);
            }



            YouTubeViewerUpdated?.Invoke(youTubeViewer);
        }
    }
}
