namespace RespawnTimer
{
    using System.Collections.Generic;
    using MEC;
    using Exiled.API.Features;
    using Exiled.Events.EventArgs.Player;

    public static class EventHandler
    {
        public static void OnGenerated()
        {
            if (Main.RespawnTimer.Instance.Config.ReloadTimerEachRound)
                Main.RespawnTimer.Instance.OnReloaded();
        }

        public static void OnVerified(VerifiedEventArgs ev)
        {
            ev.Player.GameObject.AddComponent<SpectatorPlayerComponent>();
        }
    }
}