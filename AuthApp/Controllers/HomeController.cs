using AuthApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using JikanDotNet;
using AuthApp.Services;
using AuthApp.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace AuthApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<HomeController> _logger;
        private readonly IJikanService _jikanService;
        private readonly UserManager<AppUser> _userManager;

        public HomeController(
            IServiceProvider serviceProvider,
            ILogger<HomeController> logger,
            ApplicationDbContext dbContext,
            IJikanService jikanService,
            UserManager<AppUser> userManager)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _jikanService = jikanService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("You are in Home/Index now");
            return View();
        }

        public IActionResult Account()
        {
            var dbContext = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var userAnimeLabels = dbContext.UserAnimeLabels
                    .Where(x => x.UserId == userId)
                    .Include(x => x.Label)
                    .Select(x => new LabeledAnime 
                    {
                        LabelName = x.Label.Name,
                        AnimeName = _jikanService.GetAnime(x.AnimeId).Title
                    })
                    .ToList();

                var temp = userAnimeLabels.GroupBy(x => x.LabelName);
                ViewData["LikedAnime"] = userAnimeLabels.GroupBy(x => x.LabelName);
            }
            
            return View();
        }
        
        [HttpPost]
        public JsonResult MakeFriends(string id)
        {
            var dbContext = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();

            AppUser currentUserFromManager = _userManager.GetUserAsync(User).GetAwaiter().GetResult();
            var currentUser = dbContext.Users.FirstOrDefault(x => x.Id == currentUserFromManager.Id) as AppUser;
            var userToBeFriendsWith = dbContext.Users.FirstOrDefault(x => x.Id == id) as AppUser;

            if (currentUser == null || userToBeFriendsWith == null)
            {
                return new JsonResult(new {success = false });
            }

            bool madeFriends = currentUser.IAmFriendsWith.Any(x => x.FriendId == id);
            if (madeFriends)
            {
                // delete
            }
            else
            {
                // add
                currentUser.IAmFriendsWith.Add(new FriendRelation {User = currentUser, Friend = userToBeFriendsWith});
                currentUser.AreFriendsWithMe.Add(new FriendRelation {User = userToBeFriendsWith, Friend = currentUser});
            
                currentUser.IAmFriendsWith.Add(new FriendRelation {User = userToBeFriendsWith, Friend = currentUser});
                currentUser.AreFriendsWithMe.Add(new FriendRelation {User = currentUser, Friend = userToBeFriendsWith});

                dbContext.FriendRelations.Add(new FriendRelation {User = currentUser, Friend = userToBeFriendsWith});
                dbContext.FriendRelations.Add(new FriendRelation {User = userToBeFriendsWith, Friend = currentUser});
            }

            dbContext.SaveChanges();

            return new JsonResult(new {success = true, madeFriends = madeFriends, data = "some test data"});
        }

        [HttpPost]
        public JsonResult LikeAnime(int animeId)
        {
            var dbContext = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();

            try
            {
                AnimeLabel likeLabel = dbContext.AnimeLabels.First(x => x.Name == "Liked");
                bool added = LabelAnime(animeId, likeLabel);
                return new JsonResult(new {success = true, added = added});
            }
            catch (Exception e)
            {
                return new JsonResult(new {success = false, error = e.Message});
            }
        }
        
        [HttpPost]
        public JsonResult DislikeAnime(int animeId)
        {
            var dbContext = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();

            try
            {
                AnimeLabel likeLabel = dbContext.AnimeLabels.First(x => x.Name == "Disliked");
                bool added = LabelAnime(animeId, likeLabel);
                return new JsonResult(new {success = true, added = added});
            }
            catch (Exception e)
            {
                return new JsonResult(new {success = false, error = e.Message});
            }
        }

        /// <summary>
        /// Adds or removes label from anime
        /// </summary>
        /// <returns>true if label was added, false if label was removed</returns>
        private bool LabelAnime(int animeId, AnimeLabel label)
        {
            var dbContext = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();

            AppUser currentUserFromManager = _userManager.GetUserAsync(User).GetAwaiter().GetResult();
            var currentUser = dbContext.Users.FirstOrDefault(x => x.Id == currentUserFromManager.Id) as AppUser;

            var anime = _jikanService.GetAnime(animeId);
            dbContext.Animes.AddIfNotExists(anime);

            var foundLabel = dbContext.UserAnimeLabels.FirstOrDefault(x => x.UserId == currentUser.Id && x.AnimeId == animeId && x.LabelId == label.Id);
            bool flag = foundLabel == null;
            if (flag)
            {
                dbContext.UserAnimeLabels.Add(new UserAnimeLabel {User = currentUser, AnimeId = animeId, LabelId = label.Id});
            }
            else
            {
                dbContext.UserAnimeLabels.Remove(foundLabel);
            }
            
            dbContext.SaveChanges();
            return flag;
        }
        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
