using AuthApp.Controllers;
using AuthApp.Data;
using JikanDotNet;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace AuthApp
{
    public class ViewHelper
    {
        private readonly ApplicationDbContext _dbContext;

        public ViewHelper(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string[] GetAnimeLabels()
        {
            return _dbContext.AnimeLabels.Select(x => x.Name).ToArray();
        }
    }
}