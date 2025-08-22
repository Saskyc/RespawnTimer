using System.Collections.Generic;
using Exiled.API.Features;
using JetBrains.Annotations;
using RespawnTimer.API;
using RespawnTimer.API.Features;
using UnityEngine;

namespace RespawnTimer;

public class TimerPlayer
{
    public Player Player { get; set; }
    
    public static List<TimerPlayer> List { get; set; } = [];

    public bool WasSentBefore { get; set; } = false;

    public UnCld HintCooldown { get; set; } = Main.RespawnTimer.Instance.Config.NewHintDelay;
    public UnCld RandomColorCooldown { get; set; } = Main.RespawnTimer.Instance.Config.RandomColorDelay;
    
    public string SelectedHint { get; set; } = "";
    [CanBeNull] public string RandomColor { get; set; } = null;
    
    [CanBeNull]
    public TimerManager Manager {
        get
        {
            TimerManager.TryGetTimerForPlayer(Player, out var manager);
            return manager;
        } 
    }

    public void SelectNewHint()
    {
        SelectedHint = Manager.Hints.RandomItem();
    }
    
    public static TimerPlayer Get(Player player)
    {
        foreach (var list in List)
        {
            if (list.Player == player) return list;
        }
        
        var timerPlayer = new TimerPlayer();
        timerPlayer.Player = player;
        List.Add(timerPlayer);
        return timerPlayer;
    }
}