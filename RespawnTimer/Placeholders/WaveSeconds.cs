using System;
using Exiled.API.Features;
using RespawnTimer.API;
using RespawnTimer.API.Extensions;

namespace RespawnTimer.Placeholders;

public class WaveSeconds : Placeholder
{
    public override string ThePlaceholder { get; set; } = "{seconds}";
    public override string ToReplace(Player player)
    {
        var wave = WaveManagerExtension.WaveNotPausedWithLowestTime();
        if (wave is null) return "";
        var timet = WaveManagerExtension.RespawnIn(wave);

        if (timet is null)
            return "";
        
        var time = timet.Value;
        
        int seconds = (int)Math.Round(time % 60);
        return $"{(Manager.Properties.LeadingZeros && seconds < 10 ? "0" : string.Empty)}{seconds}";
    }
}

