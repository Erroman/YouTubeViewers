﻿using System;
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
        private readonly YouTubeViewersDbContextFactory _contextFactory;

        public DeleteYouTubeViewerCommand(YouTubeViewersDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task Execute(Guid id)
        {
                using (YouTubeViewersDbContext context = _contextFactory.Create())
                {
                //throw new Exception();
                await Task.Delay(5000);

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
