using System.Collections.Generic;
using RespawnTimer.API.Features.ExternalTeams;

namespace RespawnTimer.API
{
    public static class API
    {
        public static List<string> TimerHidden { get; } = new();
        public static bool SerpentsHandSpawnable => SerpentsHandTeam.IsSpawnable;

        public static bool UiuSpawnable => UiuTeam.IsSpawnable;

        internal static readonly ExternalTeamChecker SerpentsHandTeam = new SerpentsHandTeam();
        internal static readonly ExternalTeamChecker UiuTeam = new UiuTeam();
    }
}