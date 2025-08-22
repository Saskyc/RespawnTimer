using Exiled.API.Features;
using RespawnTimer.API;
using RespawnTimer.API.Extensions;

namespace RespawnTimer.Placeholders;

public class WaveReady : Placeholder
{
    public override string ThePlaceholder { get; set; } = "{wave_ready}";
    public override string ToReplace(Player player)
    {
        var wave = WaveManagerExtension.WaveNotPausedWithLowestTime();
        if(wave is null) return "";
        var isReady = wave.IsWaveReady();
        if (isReady is null) return "";
        if (!isReady.Value) return "Yes";
        return "No";
    }
}