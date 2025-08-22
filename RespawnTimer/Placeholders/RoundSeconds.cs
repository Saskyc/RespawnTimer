using Exiled.API.Features;
using GameCore;
using RespawnTimer.API;
using Utf8Json.Internal.DoubleConversion;

namespace RespawnTimer.Placeholders;

public class RoundSeconds : Placeholder
{
    public override string ThePlaceholder { get; set; } = "{round_seconds}";
    public override string ToReplace(Player player)
    {
        int seconds = RoundStart.RoundLength.Seconds;
        return $"{(Manager.Properties.LeadingZeros && seconds < 10 ? "0" : string.Empty)}{seconds}";
    }
}