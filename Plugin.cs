using System;
using Exiled.API.Features;
using ServerHandlers = Exiled.Events.Handlers.Server;

namespace CustomRespawnSystem
{
    public class Plugin : Plugin<Config>
    {
        public static Plugin Instance { get; private set; }
        public EventHandlers EventHandler { get; private set; }
        public override string Name { get; } = "Custom Respawn System";
        public override string Author { get; } = "Raging Tornado";
        public override Version Version => new Version(1, 2, 0);
        public override void OnEnabled()
        {
            Instance = this;
            EventHandler = new EventHandlers();
            ServerHandlers.EndingRound += EventHandler.EndingRound;
            ServerHandlers.RespawningTeam += EventHandler.RespawningTeam;
            ServerHandlers.RoundStarted += EventHandler.RoundStarted;
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            ServerHandlers.EndingRound -= EventHandler.EndingRound;
            ServerHandlers.RespawningTeam -= EventHandler.RespawningTeam;
            ServerHandlers.RoundStarted -= EventHandler.RoundStarted;

            EventHandler = null;
            Instance = null;
            base.OnDisabled();
        }
    }
}
