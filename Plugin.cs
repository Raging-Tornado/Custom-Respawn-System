using Exiled.API.Features;
using Exiled.Events.EventArgs.Map;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Server;
using Exiled.Events.EventArgs.Warhead;
using Exiled.Events.Features;
using Exiled.Events.Handlers;
using System;

namespace CustomRespawnSystem
{
    public class Plugin : Plugin<Config>
    {
        public static Plugin Instance { get; private set; }
        public override string Name { get; } = "Custom Respawn System";
        public override string Author { get; } = "Raging Tornado";
        public override Version Version => new Version(1, 0, 0);
        public override void OnEnabled()
        {
            base.OnEnabled();
            Plugin.Instance = this;
            this.eventHandler = new EventHandlers();
            Exiled.Events.Handlers.Server.RoundStarted += new CustomEventHandler(this.eventHandler.RoundStarted);
            Exiled.Events.Handlers.Server.EndingRound += new CustomEventHandler<EndingRoundEventArgs>(this.eventHandler.EndingRound);
            Exiled.Events.Handlers.Server.RespawningTeam += new CustomEventHandler<RespawningTeamEventArgs>(this.eventHandler.RespawningTeam);
        }
        public override void OnDisabled()
        {
            base.OnDisabled();
            Plugin.Instance = null;
            this.eventHandler = null;
            Exiled.Events.Handlers.Server.RoundStarted -= new CustomEventHandler(this.eventHandler.RoundStarted);
            Exiled.Events.Handlers.Server.EndingRound -= new CustomEventHandler<EndingRoundEventArgs>(this.eventHandler.EndingRound);
            Exiled.Events.Handlers.Server.RespawningTeam -= new CustomEventHandler<RespawningTeamEventArgs>(this.eventHandler.RespawningTeam);
        }
        private EventHandlers eventHandler;
    }
}
