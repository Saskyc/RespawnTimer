using Exiled.API.Features;
using RespawnTimer.API;

namespace RespawnTimer.Placeholders;

public class Hint : Placeholder
{
    public override string ThePlaceholder { get; set; } = "{hint}";
    public override string ToReplace(Player player)
    {
        return TimerPlayer.SelectedHint;
    }
}