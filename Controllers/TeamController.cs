using Microsoft.AspNetCore.Mvc;
using NflTeamsSessionState.Models;

namespace NflTeamsSessionState.Controllers
{
    public class TeamController : Controller
    {
        public IActionResult Index()
        {
            var teams = TeamList.GetTeams();
            return View(teams); // Views/Team/Index.cshtml
        }

        [HttpPost]
        public IActionResult AddToFavorites(string teamName)
        {
            var team = TeamList.GetTeams().FirstOrDefault(t => t.Name == teamName);
            if (team != null)
            {
                var nflSession = new NFLSession(HttpContext.Session);
                var favorites = nflSession.GetMyTeams();
                if (!favorites.Any(t => t.Name == team.Name))
                {
                    favorites.Add(team);
                    nflSession.SetMyTeams(favorites);
                }
            }

            return RedirectToAction("Index", "Favorites");
        }
    }
}
