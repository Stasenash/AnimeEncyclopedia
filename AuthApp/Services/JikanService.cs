using AuthApp.Data;
using JikanDotNet;
using JikanDotNet.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AuthApp.Services
{
    public class JikanService : IJikanService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<SyncService> _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly IJikan _jikan;

        public JikanService(
            IServiceProvider serviceProvider,
            ILogger<SyncService> logger,
            ApplicationDbContext dbContext,
            IJikan jikan)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _dbContext = dbContext;
            _jikan = jikan;
        }

        public Anime GetAnime(long animeId)
        {
            var context = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var anime = context.Animes.FirstOrDefault(x => x.MalId == animeId);
            if (anime == null)
            {
                try
                {
                    anime = _jikan.GetAnime(animeId).GetAwaiter().GetResult();

                    context.Animes.Add(anime);
                    var scheduled = context.ScheduledAnimes.FirstOrDefault(x => x.AnimeId == anime.MalId);
                    if (scheduled != null)
                    {
                        context.ScheduledAnimes.Remove(scheduled);
                    }
                    context.SaveChanges();
                }
                catch (JikanRequestException je)
                {
                    // wait and try again
                    Thread.Sleep(500);
                    anime = GetAnime(animeId);
                }
                catch (Exception e)
                {
                    _logger.LogError($"{nameof(JikanService)}: something went wrong.", e.Message, e.InnerException);
                }
            }

            return anime;
        }

        public AnimeSearchResult SearchAnime(string part)
        {
            return _jikan.SearchAnime(part).GetAwaiter().GetResult();
        }
    }
}
