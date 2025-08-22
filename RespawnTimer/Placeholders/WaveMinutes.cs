using Exiled.API.Features;
using RespawnTimer.API;
using RespawnTimer.API.Extensions;

namespace RespawnTimer.Placeholders;

public class WaveMinutes : Placeholder
{
    public override string ThePlaceholder { get; set; } = "{minutes}";
    public override string ToReplace(Player player)
    {
        var wave = WaveManagerExtension.WaveNotPausedWithLowestTime();
        if (wave is null) return "";
        var timet = WaveManagerExtension.RespawnIn(wave);
        if (timet is null) return "";
        
        var time = timet.Value;
            
        int minutes = (int)time / 60;
        return $"{(Manager.Properties.LeadingZeros && minutes < 10 ? "0" : string.Empty)}{minutes}";
    }
}