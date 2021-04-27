using AuthApp.Data;
using AuthApp.Utils;
using JikanDotNet;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AuthApp.Services
{
    public class SyncService : IHostedService, IDisposable
    {
        private string tag = nameof(SyncService);

        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<SyncService> _logger;
        private readonly IJikan _jikan;
        private Timer _timer;

        public SyncService(
            IServiceProvider serviceProvider,
            ILogger<SyncService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _jikan = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IJikan>();
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"{tag}: service is running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            var dbContext = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var oldestRecord = dbContext.ScheduledAnimes.OrderBy(x => x.AddedDateTime).FirstOrDefault();
            Anime anime = null;
            if (oldestRecord != null)
            {
                try
                {
                    anime = _jikan.GetAnime(oldestRecord.AnimeId).GetAwaiter().GetResult();
                
                    dbContext.Animes.AddIfNotExists(anime);
                    dbContext.ScheduledAnimes.Remove(oldestRecord);

                    dbContext.SaveChanges();

                    _logger.LogInformation($"{tag}: downloaded anime \"{anime?.Title}\"");
                }
                catch (Exception e)
                {
                    _logger.LogError($"{tag}: error.", e.Message, e.InnerException);

                    dbContext.ScheduledAnimes.AddIfNotExists(oldestRecord);
                    dbContext.SaveChanges();
                }
            }
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"{tag}: service is stopping.");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
