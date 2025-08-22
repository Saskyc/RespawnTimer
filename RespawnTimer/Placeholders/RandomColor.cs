using Exiled.API.Features;
using RespawnTimer.API;
using UnityEngine;

namespace RespawnTimer.Placeholders;

public class RandomColor : Placeholder
{
    public override string ThePlaceholder { get; set; } = "{RANDOM_COLOR}";
    public override string ToReplace(Player player)
    {
        if(TimerPlayer.RandomColor is null)
            SwitchColor(TimerPlayer);
        return TimerPlayer.RandomColor;
    }

    public static void SwitchColor(TimerPlayer player)
    {
        player.RandomColor = $"#{Random.Range(0x0, 0xFFFFFF):X6}";
    }
}