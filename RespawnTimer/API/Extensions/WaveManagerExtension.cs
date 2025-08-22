using JetBrains.Annotations;
using Respawning;
using Respawning.Waves;
using Respawning.Waves.Generic;

namespace RespawnTimer.API.Extensions;

public static class WaveManagerExtension
{
    [CanBeNull]
    public static TimeBasedWave GetTimeBasedWave(this SpawnableWaveBase wave)
    {
        if (wave is null) return null;
        if(wave is not TimeBasedWave timeBasedWave) return null;
        return timeBasedWave;
        
    }
    
    public static ILimitedWave GetLimitedWave(this SpawnableWaveBase wave)
    {
        var tiemBasedWave = GetTimeBasedWave(wave);
        if (tiemBasedWave is null) return null;
        if (tiemBasedWave is not ILimitedWave limitedWave) return null;
        return limitedWave;
    }
    
    public static int? GetRespawnTokens(this SpawnableWaveBase wave)
    {
        var limitedWave = GetLimitedWave(wave);
        if (limitedWave is null) return null;
        return limitedWave.RespawnTokens;
    }
    
    public static bool? IsWavePaused(this SpawnableWaveBase wave)
    {
        var timeBasedWave = GetTimeBasedWave(wave);
        if (timeBasedWave is null) return null;
        return timeBasedWave.Timer.IsPaused;
    }
    
    public static bool? IsWaveReady(this SpawnableWaveBase wave)
    {
        var timeBasedWave = GetTimeBasedWave(wave);
        if (timeBasedWave is null) return null;
        return timeBasedWave.Timer.IsReadyToSpawn;
    }
    
    public static float? GetWaveTime(this SpawnableWaveBase wave)
    {
        var timeBasedWave = GetTimeBasedWave(wave);
        if (timeBasedWave is null) return null;
        return timeBasedWave.Timer.TimeLeft;
    }
    
    public static float GetWaveTime(this TimeBasedWave wave)
    {
        return wave.Timer.TimeLeft;
    }
    
    public static bool IsWavePaused(this TimeBasedWave wave)
    {
        return wave.Timer.IsPaused;
    }
    
    public static SpawnableWaveBase? WaveNotPausedWithLowestTime()
    {
        float? timeToRespawnIn = null;
        SpawnableWaveBase? toReturnWave = null;
        foreach (var wave in WaveManager.Waves)
        {
            var timeBasedWave = wave.GetTimeBasedWave();
            if (timeBasedWave is null) continue;
            
            var isPaused = timeBasedWave.IsWavePaused();
            if(isPaused) continue;
            
            var waveTime = timeBasedWave.GetWaveTime();
            
            if (!timeToRespawnIn.HasValue || timeToRespawnIn.Value > waveTime)
            {
                timeToRespawnIn = waveTime;
                toReturnWave = wave;
            }
        }
        
        return toReturnWave;
    }
    
    public static SpawnableWaveBase? WaveWithLowestTime()
    {
        float? respawnIn = null;
        SpawnableWaveBase? twave = null;
        foreach (var wave in WaveManager.Waves)
        {
            var s = wave.GetWaveTime();
            if (!s.HasValue) continue;
            if (!respawnIn.HasValue || respawnIn.Value > s.Value)
            {
                respawnIn = s.Value;
                twave = wave;
            }
        }
        
        return twave;
    }
    
    public static float? RespawnIn(SpawnableWaveBase wave)
    {
        var waveWithLowestTime = wave;
        if(waveWithLowestTime is null) return null;
        var timeBasedWave = waveWithLowestTime.GetTimeBasedWave();
        if (timeBasedWave is null) return null;
        return timeBasedWave.GetWaveTime();
    }
    
    public static bool AllPaused()
    {
        foreach (var wave in WaveManager.Waves)
            if (wave.IsWavePaused().HasValue && !wave.IsWavePaused().Value) return false;
        return true;
    }
}