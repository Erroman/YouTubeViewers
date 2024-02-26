using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouTubeViewers.Domain.Commands;
using YouTubeViewers.Domain.Models;
using YouTubeViewers.EntityFramework;
using YouTubeViewers.EntityFramework.DTOs;

namespace YouTubeViewers.EntityFramework.Commands
{
    public class CreateYouTubeViewerCommand : ICreateYouTubeViewerCommand    
    {
        private readonly YouTubeViewersDBContextFactory _contextFactory;

        public CreateYouTubeViewerCommand(YouTubeViewersDBContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task Execute(YouTubeViewer youTubeViewer)
        {
            using (YouTubeViewersDBContext context = _contextFactory.Create()) 
            { 
                YouTubeViewerDto youTubeViewersDtos = new YouTubeViewerDto() 
                {
                    Id = youTubeViewer.Id,
                    Username = youTubeViewer.Username,
                    IsSubscribed = youTubeViewer.IsSubscribed,
                    IsMember = youTubeViewer.IsMember
                };
                context.YouTubeViewers.Add(youTubeViewersDtos);
                await context.SaveChangesAsync();

            }

                
            }
    }
}
