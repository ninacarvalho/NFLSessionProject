using Microsoft.AspNetCore.Mvc;
using NflTeamsSessionState.Models;

namespace NflTeamsSessionState.Controllers
{
    public class TeamController : Controller
    {

        public IActionResult Index()
        {
            var nflSession = new NFLSession(HttpContext.Session);

            // Check if session is empty
            if (nflSession.GetMyTeamCount() == 0)
            {
                var cookies = new NFLCookies(Request.Cookies);
                string[] ids = cookies.GetMyTeamIds();

                var teamsFromCookie = TeamList.GetTeams()
                    .Where(t => ids.Contains(t.Name))
                    .ToList();

                if (teamsFromCookie.Any())
                {
                    nflSession.SetMyTeams(teamsFromCookie);
                }
            }

            var teams = TeamList.GetTeams();
            return View(teams);
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

                    // Save to cookie
                    var cookies = new NFLCookies(Response.Cookies);
                    cookies.SetMyTeamIds(favorites);
                }
            }

            return RedirectToAction("Index", "Favorites");
        }

    }
}
