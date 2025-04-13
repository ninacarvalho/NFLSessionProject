using Microsoft.AspNetCore.Http;
using NflTeamsSessionState.Models;
using System.Collections.Generic;

public class NFLSession
{
    private const string TeamsKey = "myteams";
    private const string CountKey = "teamcount";

    private ISession session;

    public NFLSession(ISession session)
    {
        this.session = session;
    }

    public void SetMyTeams(List<Team> teams)
    {
        session.SetObject(TeamsKey, teams);
        session.SetInt32(CountKey, teams.Count);
    }

    public List<Team> GetMyTeams() =>
        session.GetObject<List<Team>>(TeamsKey) ?? new List<Team>();

    public int GetMyTeamCount() =>
        session.GetInt32(CountKey) ?? 0;

    public void RemoveMyTeams()
    {
        session.Remove(TeamsKey);
        session.Remove(CountKey);
    }
}
