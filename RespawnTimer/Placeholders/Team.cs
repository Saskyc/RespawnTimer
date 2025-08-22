using Exiled.API.Features;
using GameCore;
using RespawnTimer.API;
using Utf8Json.Internal.DoubleConversion;

namespace RespawnTimer.Placeholders;

public class Team : Placeholder
{
    public override string ThePlaceholder { get; set; } = "{team}";
    public override string ToReplace(Player player)
    {
        return Respawn.NextKnownSpawnableFaction.ToString();
    }
}