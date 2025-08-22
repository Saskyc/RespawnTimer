using System;
using System.Linq;
using Exiled.API.Features;
using PlayerRoles;
using RespawnTimer.API.Features;
using RespawnTimer.Main;
using RespawnTimer.Placeholders;
using UnityEngine;

namespace RespawnTimer;

public class SpectatorPlayerComponent : MonoBehaviour
{
    public Player Player;
    public void Awake()
    {
        Player = Player.Get(gameObject);
        if (Player is null)
        {
            Log.Info("Death");
            Destroy(this);
        }
    }
    
    public float cooldown = 3f;
    public float timer = 0f;
    public bool ready = false;
    
    public void OnDestroy()
    {
        Log.Info("Death :(");
    }

    public void Update()
    {
        if (!Round.IsStarted)
            return;

        if (Player.Role.Type != RoleTypeId.Spectator)
            return;
        
        /*if (Player.IsAlive || !Player.SessionVariables.ContainsKey("IsGhost"))
            return;

        if (Player.IsOverwatchEnabled && Main.RespawnTimer.Instance.Config.HideTimerForOverwatch)
            return;

        if (API.API.TimerHidden.Contains(Player.UserId))
            return;

        if (EventHandler.PlayerDeathDictionary.ContainsKey(Player))
            return;*/

        if (RespawnTimer.Main.RespawnTimer.Instance.Config.ReloadInRealTime)
            Helper.ReloadTimer();
        
        TimerPlayer timerPlayer = TimerPlayer.Get(Player);
        timerPlayer.HintCooldown.AppendTime();
        
        var manager = timerPlayer.Manager;
        if (manager is null)
            return;

        string text;
        
        timerPlayer.RandomColorCooldown.AppendTime();
        if (timerPlayer.RandomColorCooldown.IsReady)
        {
            RandomColor.SwitchColor(timerPlayer);
            timerPlayer.RandomColorCooldown.Reset();
        }
        
        if (!timerPlayer.WasSentBefore)
        {
            timerPlayer.SelectNewHint();
            text = manager.GetText(timerPlayer);
            Player.ShowHint(text);
            timerPlayer.WasSentBefore = true;
            return;
        }

        if (timerPlayer.HintCooldown.IsReady)
        {
            timerPlayer.SelectNewHint();
            timerPlayer.HintCooldown.Reset();
        }
        
        text = manager.GetText(timerPlayer);
        Player.ShowHint(text);
    }
}