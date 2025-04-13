using Microsoft.AspNetCore.Http;
using NflTeamsSessionState.Models;
using System;
using System.Collections.Generic;
using System.Linq;

public class NFLCookies
{
    private const string MyTeamsKey = "myteams";
    private const string Delimiter = "-";

    private IRequestCookieCollection requestCookies;
    private IResponseCookies responseCookies;

    public NFLCookies(IRequestCookieCollection cookies)
    {
        requestCookies = cookies;
    }

    public NFLCookies(IResponseCookies cookies)
    {
        responseCookies = cookies;
    }

    public void SetMyTeamIds(List<Team> myteams)
    {
        var ids = myteams.Select(t => t.Name).ToList();
        string idsString = string.Join(Delimiter, ids);

        var options = new CookieOptions
        {
            Expires = DateTime.Now.AddDays(30)
        };

        RemoveMyTeamIds();
        responseCookies.Append(MyTeamsKey, idsString, options);
    }

    public string[] GetMyTeamIds()
    {
        string cookie = requestCookies[MyTeamsKey];
        return string.IsNullOrEmpty(cookie)
            ? Array.Empty<string>()
            : cookie.Split(Delimiter);
    }

    public void RemoveMyTeamIds()
    {
        responseCookies.Delete(MyTeamsKey);
    }
}
