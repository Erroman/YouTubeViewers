using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTubeViewers.EntityFramework
{
    public class YouTubeViewersDBContextFactory
    {
        private readonly DbContextOptions _options;

        public YouTubeViewersDBContextFactory(DbContextOptions options)
        {
            _options = options;
        }


        public YouTubeViewersDBContext Create()
        {
   
            return new YouTubeViewersDBContext(options);
        }
    }
}
