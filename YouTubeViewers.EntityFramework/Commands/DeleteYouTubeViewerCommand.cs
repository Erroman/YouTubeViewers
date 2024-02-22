using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouTubeViewers.Domain.Commands;
using YouTubeViewers.Domain.Models;
using YouTubeViewers.EntityFramework.DTOs;

namespace YouTubeViewers.EntityFramework.Commands
{
    public  class DeleteYouTubeViewerCommand : IDeleteYouTubeViewerCommand
    {
        private readonly YouTubeViewersDBContextFactory _contextFactory;

        public DeleteYouTubeViewerCommand(YouTubeViewersDBContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task Execute(Guid id)
        {
                using (YouTubeViewersDBContext context = _contextFactory.Create())
                {
                    YouTubeViewerDto youTubeViewersDtos = new YouTubeViewerDto()
                    {
                        Id = id,
                    };
                    context.YouTubeViewers.Remove(youTubeViewersDtos);
                    await context.SaveChangesAsync();

                }
        }
    }
}
