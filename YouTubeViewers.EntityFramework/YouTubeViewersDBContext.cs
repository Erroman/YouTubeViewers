using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouTubeViewers.Domain.Models;
using YouTubeViewers.EntityFramework.DTOs;

namespace YouTubeViewers.EntityFramework
{
    public class YouTubeViewersDBContext : DbContext
    {
        public DbSet<YouTubeViewerDto> YouTubeViewers { get; set; }
    }
}
