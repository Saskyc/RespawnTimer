using System.Linq;
using Exiled.API.Features;
using RespawnTimer.API;

namespace RespawnTimer.Placeholders;

public class SpectatorCount : Placeholder
{
    public override string ThePlaceholder { get; set; } = "{spectators_num}";
    public override string ToReplace(Player player)
    {
        return Exiled.API.Features.Player.List
            .Where(x => !x.IsAlive || x.SessionVariables.ContainsKey("IsGhost")).ToList().Count.ToString();
    }
}