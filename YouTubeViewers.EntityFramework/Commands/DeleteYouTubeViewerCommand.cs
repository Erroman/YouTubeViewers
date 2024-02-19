using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTubeViewers.EntityFramework.Commands
{
    public  class DeleteYouTubeViewerCommand
    {
        private readonly YouTubeViewersDBContextFactory _contextFactory;

        public GetAllYouTubeViewersQuery(YouTubeViewersDBContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
