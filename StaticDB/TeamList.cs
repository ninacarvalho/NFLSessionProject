using System.Collections.Generic;

namespace NflTeamsSessionState.Models
{
    public static class TeamList
    {
        public static List<Team> GetTeams() => new List<Team>
        {
            new Team { Name = "Packers", City = "Green Bay" },
            new Team { Name = "Bears", City = "Chicago" },
            new Team { Name = "Lions", City = "Detroit" },
            new Team { Name = "Vikings", City = "Minnesota" }
        };
    }
}
