using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthApp.Data;
using AuthApp.Models;
using AuthApp.Utils;
using JikanDotNet;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;
using AuthApp.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AuthApp.Controllers
{
    public class SearchController : Controller
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<SearchController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly IJikanService _jikanService;

        public SearchController(
            IServiceProvider serviceProvider,
            ILogger<SearchController> logger,
            UserManager<AppUser> userManager,
            IJikanService jikanService)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _userManager = userManager;
            _jikanService = jikanService;
        }

        public IActionResult Index()
        {
            var dbContext = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();
            TempData["animeLabels"] = dbContext.AnimeLabels.Select(x => x.Name).ToArray();
            TempData.Keep("animeLabels");
            return View();
        }

        public IActionResult SearchPerson(string userName)
        {
            var dbContext = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();

            AppUser currentUser = _userManager.GetUserAsync(User).GetAwaiter().GetResult();
            var friends = dbContext.FriendRelations.Where(x => x.UserId == currentUser.Id).Select(x => x.FriendId).ToList();
            
            var searchResult = _userManager.Users
                .Where(x => x.UserName.Contains(userName) && x.UserName != User.Identity.Name)
                .Select(x => new SearchResult {Id = x.Id, Name = x.UserName})
                .ToList();

            foreach (SearchResult result in searchResult.Where(result => friends.Contains(result.Id)))
            {
                result.Labels = new List<string> {"Friend"};
            }
            
            return new PartialViewResult
            {
                ViewName = "~/Views/Search/PeoplePartial.cshtml",
                ViewData = new ViewDataDictionary<List<SearchResult>>(ViewData, searchResult)
            };
        }

        public IActionResult SearchAnime(string animeTitle)
        {
            var animeSearchEntries = _jikanService
                .SearchAnime(animeTitle).Results
                .ToList();
            
            ScheduleAnimes(animeSearchEntries);

            var searchResults = ConvertToModel(animeSearchEntries);
            
            return new PartialViewResult
            {
                ViewName = "~/Views/Search/AnimePartial.cshtml",
                ViewData = new ViewDataDictionary<List<SearchResult>>(ViewData, searchResults)
            };
        }

        private List<SearchResult> ConvertToModel(List<AnimeSearchEntry> animeSearchEntries)
        {
            var dbContext = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var searchResults = animeSearchEntries.Select(x => new SearchResult {Id = x.MalId.ToString(), Name = x.Title}).ToList();
            
            AppUser currentUser = _userManager.GetUserAsync(User).GetAwaiter().GetResult();
            var labeledAnime = dbContext.UserAnimeLabels
                .Include(x => x.Label)
                .Where(x => x.UserId == currentUser.Id)
                .ToList();

            foreach (SearchResult result in searchResults)
            {
                var existingAnimeLabels = labeledAnime.Where(x => x.AnimeId.ToString() == result.Id).ToList();
                if (existingAnimeLabels.Count > 0)
                {
                    result.Labels = existingAnimeLabels.Select(x => x.Label.Name).ToList();
                }
            }

            return searchResults;
        }

        private async void ScheduleAnimes(List<AnimeSearchEntry> animeSearchEntries)
        {
            var dbContext = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var scheduledAnimes = animeSearchEntries.Select(x => 
                new ScheduledAnime
                {
                    AnimeId = x.MalId, 
                    AddedDateTime = DateTime.Now
                });

            foreach (ScheduledAnime scheduledAnime in scheduledAnimes)
            {
                dbContext.ScheduledAnimes.AddIfNotExists(scheduledAnime);

                var anime = animeSearchEntries.First(x => x.MalId == scheduledAnime.AnimeId);
                _logger.LogInformation($"Anime {anime.Title} [Id = {anime.MalId}] was added to queue for downloading");
            }

            dbContext.SaveChangesAsync();
        }
    }
}