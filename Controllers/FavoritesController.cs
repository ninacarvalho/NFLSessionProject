using Microsoft.AspNetCore.Mvc;
using NflTeamsSessionState.Models;

public class FavoritesController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        var nflSession = new NFLSession(HttpContext.Session);
        var teams = nflSession.GetMyTeams();
        return View(teams); // Views/Favorites/Index.cshtml
    }

    [HttpPost]
    public IActionResult Delete()
    {
        var session = new NFLSession(HttpContext.Session);
        session.RemoveMyTeams();

        var cookies = new NFLCookies(Response.Cookies);
        cookies.RemoveMyTeamIds();

        TempData["message"] = "Favorite teams cleared!";
        return RedirectToAction("Index", "Team");
    }

}
