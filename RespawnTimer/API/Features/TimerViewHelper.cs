using Respawning.Waves;
using RespawnTimer.API.Extensions;

namespace RespawnTimer.API.Features;

using System;
using System.Globalization;
using System.Linq;
using GameCore;
using PlayerRoles;
using PlayerRoles.PlayableScps.Scp079;
using UnityEngine;
using Exiled.API.Enums;
using Exiled.API.Features;

public partial class TimerView
{
    private void SetAllProperties(int? spectatorCount = null)
    {
        SetRoundTime();
        SetMinutesAndSeconds();
        SetSpawnableTeam();
        SetSpectatorCountAndSpawnChance(spectatorCount);
        SetWarheadStatus();
        SetGeneratorCount();
        SetTpsAndTickrate();
        SetHint();
    }

    private void SetRoundTime()
    {
        int minutes = RoundStart.RoundLength.Minutes;
        StringBuilder.Replace("{round_minutes}", $"{(Properties.LeadingZeros && minutes < 10 ? "0" : string.Empty)}{minutes}");

        int seconds = RoundStart.RoundLength.Seconds;
        StringBuilder.Replace("{round_seconds}", $"{(Properties.LeadingZeros && seconds < 10 ? "0" : string.Empty)}{seconds}");
    }

    private void SetMinutesAndSeconds()
    {
        //TimeSpan time = TimeSpan.FromSeconds(RespawnManager.Singleton._timeForNextSequence - RespawnManager.Singleton._stopwatch.Elapsed.TotalSeconds);

        var timet = WaveManagerExtension.RespawnIn();
        if (timet is null) return;
        
        var time = timet.Value;

        int minutes = (int)time / 60;
        StringBuilder.Replace("{minutes}", $"{(Properties.LeadingZeros && minutes < 10 ? "0" : string.Empty)}{minutes}");
            
        int seconds = (int)Math.Round(time % 60);
        StringBuilder.Replace("{seconds}", $"{(Properties.LeadingZeros && seconds < 10 ? "0" : string.Empty)}{seconds}");
    }

    /// <summary>
    /// This is logic for API such as Serpents Hand & UIU, should replace them though the API
    /// is not good at all, so I've said fuck it and delted this.
    /// </summary>
    private void SetSpawnableTeam()
    {
        var nextKnownWave = Respawn.NextKnownSpawnableFaction;
    }

    private void SetSpectatorCountAndSpawnChance(int? spectatorCount = null)
    {
        
        StringBuilder.Replace("{spectators_num}", spectatorCount?.ToString() ?? Player.List.Count(x => x.Role.Team == Team.Dead && !x.IsOverwatchEnabled).ToString());
        // Backwards compatibility
        StringBuilder.Replace("{ntf_tickets_num}", "{ntf_spawn_chance}");
        StringBuilder.Replace("{ci_tickets_num}", "{ci_spawn_chance}");
        //
        
        if(WaveManagerGenericHelper.GetRespawnTokens<NtfSpawnWave>().HasValue)
            StringBuilder.Replace("{ntf_spawn_chance}",
                Mathf.Round(WaveManagerGenericHelper.GetRespawnTokens<NtfSpawnWave>().Value).ToString());
        
        if(WaveManagerGenericHelper.GetRespawnTokens<ChaosSpawnWave>().HasValue)
            StringBuilder.Replace("{ci_spawn_chance}", 
                Mathf.Round(WaveManagerGenericHelper.GetRespawnTokens<ChaosSpawnWave>().Value).ToString());
        
        if(WaveManagerGenericHelper.GetRespawnTokens<NtfMiniWave>().HasValue)
            StringBuilder.Replace("{mini_ntf_spawn_chance}", 
                Mathf.Round(WaveManagerGenericHelper.GetRespawnTokens<NtfMiniWave>().Value).ToString());
        
        if(WaveManagerGenericHelper.GetRespawnTokens<ChaosMiniWave>().HasValue)
            StringBuilder.Replace("{mini_ci_spawn_chance}", 
                Mathf.Round(WaveManagerGenericHelper.GetRespawnTokens<ChaosMiniWave>().Value).ToString());
    }

    private void SetWarheadStatus()
    {
        WarheadStatus warheadStatus = Warhead.Status;
        StringBuilder.Replace("{warhead_status}", Properties.WarheadStatus[warheadStatus.ToString()]);
        StringBuilder.Replace(
            "{detonation_time}",
            warheadStatus == WarheadStatus.InProgress ? Mathf.Round(Warhead.DetonationTimer).ToString(CultureInfo.InvariantCulture) : string.Empty);
    }

    private void SetGeneratorCount()
    {
        /*
        int generatorEngaged = 0;
        int generatorCount = 0;

        foreach (Generator generator in Generator.List)
        {
            if (generator.State.HasFlag(GeneratorState.Engaged))
                generatorEngaged++;

            generatorCount++;
        }

        StringBuilder.Replace("{generator_engaged}", generatorEngaged.ToString());
        StringBuilder.Replace("{generator_count}", generatorCount.ToString());
        */
        StringBuilder.Replace("{generator_engaged}", Scp079Recontainer.AllGenerators.Count(x => x.Engaged).ToString());
        StringBuilder.Replace("{generator_count}", "3");
    }

    private void SetTpsAndTickrate()
    {
        StringBuilder.Replace("{tps}", Math.Round(1.0 / Time.smoothDeltaTime).ToString(CultureInfo.InvariantCulture));
        StringBuilder.Replace("{tickrate}", Application.targetFrameRate.ToString());
    }

    private void SetHint()
    {
        if (!Hints.Any())
            return;

        StringBuilder.Replace("{hint}", Hints[HintIndex]);
    }
}