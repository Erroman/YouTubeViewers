﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouTubeViewers.Domain.Models;
using YouTubeViewers.Domain.Queries;
using YouTubeViewers.EntityFramework.DTOs;
//using YouTubeViewers.EntityFramework;

namespace YouTubeViewers.EntityFramework.Queries
{
    public class GetAllYouTubeViewersQuery : IGetAllYouTubeVIewersQuery
    {
        private readonly YouTubeViewersDBContextFactory _contextFactory;
        
        public GetAllYouTubeViewersQuery(YouTubeViewersDBContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<YouTubeViewer>> Execute()
        {
            using (YouTubeViewersDBContext context = _contextFactory.Create()) 
            {
                IEnumerable<YouTubeViewerDto> youTubeViewerDtos = await context.YouTubeViewers.ToListAsync();

                return youTubeViewerDtos.Select(y => new YouTubeViewer(y.Id,y.Username,y.IsSubscribed,y.IsMember));
            }
        }
    }
}
