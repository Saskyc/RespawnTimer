using System;
using Exiled.API.Enums;
using Exiled.API.Features;
using RespawnTimer.API;
using RespawnTimer.Configs;

namespace RespawnTimer.Main
{
    public class RespawnTimer : Plugin<Config>
    {
        public static RespawnTimer Instance;
        

        public override void OnEnabled()
        {
            if (!Config.IsEnabled)
            {
                OnDisabled();
                return;
            }

            Instance = this;
            Helper.FileManager();
            Exiled.Events.Handlers.Map.Generated += EventHandler.OnGenerated;
            Exiled.Events.Handlers.Player.Dying += EventHandler.OnDying;
            Exiled.Events.Handlers.Server.RoundStarted += EventHandler.RoundStarted;

            Helper.ReloadTimer();
            Initialize.Indeed();
            Coroutine.Start();
            
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Instance = null;
            
            Exiled.Events.Handlers.Map.Generated -= EventHandler.OnGenerated;
            Exiled.Events.Handlers.Player.Dying -= EventHandler.OnDying;
            Exiled.Events.Handlers.Server.RoundStarted -= EventHandler.RoundStarted;
            
            Coroutine.Kill();

            base.OnDisabled();
        }

        public override string Name => "RespawnTimer";
        public override string Author => "Michal78900";
        public override Version Version => new(4, 0, 5);
        public override Version RequiredExiledVersion => new(9, 8, 1);
        public override PluginPriority Priority => PluginPriority.Last;
    }
}