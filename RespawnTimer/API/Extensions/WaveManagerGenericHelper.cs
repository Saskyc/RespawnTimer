using Respawning;
using Respawning.Waves;
using Respawning.Waves.Generic;

namespace RespawnTimer.API.Extensions;

public class WaveManagerGenericHelper
{
    public static T GetWave<T>() where T : SpawnableWaveBase
    {
        foreach (var wave in WaveManager.Waves)
        {
            if(wave is T) return (T)wave;
        }
        
        return null;
    }

    public static TimeBasedWave GetTimeBasedWave<T>() where T : SpawnableWaveBase
    {
        var wave = GetWave<T>();
        if (wave is null) return null;
        if(wave is not TimeBasedWave timeBasedWave) return null;
        return timeBasedWave;
        
    }
    
    public static ILimitedWave GetLimitedWave<T>() where T : SpawnableWaveBase
    {
        var wave = GetTimeBasedWave<T>();
        if (wave is null) return null;
        if (wave is not ILimitedWave limitedWave) return null;
        return limitedWave;
    }
    
    public static int? GetRespawnTokens<T>() where T : SpawnableWaveBase
    {
        var wave = GetLimitedWave<T>();
        if (wave is null) return null;
        return wave.RespawnTokens;
    }
    
    public static bool? IsWavePaused<T>() where T : SpawnableWaveBase
    {
        var timeBasedWave = GetTimeBasedWave<T>();
        if (timeBasedWave is null) return null;
        return timeBasedWave.Timer.IsPaused;
    }
    
    public static float? GetWaveTime<T>() where T : SpawnableWaveBase
    {
        var timeBasedWave = GetTimeBasedWave<T>();
        if (timeBasedWave is null) return null;
        return timeBasedWave.Timer.TimeLeft;
    }
    
}