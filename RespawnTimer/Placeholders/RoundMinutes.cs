using Exiled.API.Features;
using GameCore;
using RespawnTimer.API;
using Utf8Json.Internal.DoubleConversion;

namespace RespawnTimer.Placeholders;

public class RoundMinutes : Placeholder
{
    public override string ThePlaceholder { get; set; } = "{round_minutes}";
    public override string ToReplace(Player player)
    {
        int minutes = RoundStart.RoundLength.Minutes;
        return $"{(Manager.Properties.LeadingZeros && minutes < 10 ? "0" : string.Empty)}{minutes}";
    }
}