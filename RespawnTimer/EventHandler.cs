namespace RespawnTimer
{
    using System.Collections.Generic;
    using MEC;
    using Exiled.API.Features;
    using Exiled.Events.EventArgs.Player;

    public static class EventHandler
    {
        public static void RoundStarted()
        {
            Coroutine.Kill();
            Coroutine.Start();
        }
        
        public static void OnGenerated()
        {
            if (Main.RespawnTimer.Instance.Config.ReloadTimerEachRound)
                Main.RespawnTimer.Instance.OnReloaded();
            
            Coroutine.Kill();
        }

        public static void OnDying(DyingEventArgs ev)
        {
            if (Main.RespawnTimer.Instance.Config.TimerDelay < 0)
                return;
            if (PlayerDeathDictionary.ContainsKey(ev.Player))
            {
                Timing.KillCoroutines(PlayerDeathDictionary[ev.Player]);
                PlayerDeathDictionary.Remove(ev.Player);
            }

            PlayerDeathDictionary.Add(ev.Player, Timing.CallDelayed(Main.RespawnTimer.Instance.Config.TimerDelay, () => PlayerDeathDictionary.Remove(ev.Player)));
        }

        public static Dictionary<Player, CoroutineHandle> PlayerDeathDictionary = new(25);
    }
}