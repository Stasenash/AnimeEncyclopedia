using JikanDotNet;

namespace AuthApp.Services
{
    public interface IJikanService
    {
        Anime GetAnime(long animeId);
        AnimeSearchResult SearchAnime(string part);
    }
}