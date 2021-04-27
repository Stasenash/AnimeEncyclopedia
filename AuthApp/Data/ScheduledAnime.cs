using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JikanDotNet;

namespace AuthApp.Data
{
    public class ScheduledAnime
    {
        public long Id { get; set; }
        public long AnimeId { get; set; }
        
        public DateTime AddedDateTime { get; set; }
    }
}
