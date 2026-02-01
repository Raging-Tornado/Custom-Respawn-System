using CustomMusics;
using Exiled.API.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;

namespace CustomRespawnSystem
{
    public sealed class Config : IConfig
    {
        [Description("Value indicating whether the plugin is enabled.")]
        public bool IsEnabled { get; set; } = true;

        [Description("Value indicating whether debug messages should be displayed in the console.")]
        public bool Debug { get; set; } = false;
        [Description("Spawn settings")]
        public int MinimumTimeToSpawn { get; set; } = 280;
        public int MaximumTimeToSpawn { get; set; } = 350;
        public int MaximumMtfRespawnAmount { get; set; } = 15;
        public int MaximumCiRespawnAmount { get; set; } = 15;
        public bool PriorityMtfRespawn { get; set; } = true;
        [Description("The ratio of MTF-to-CI tickets directly determines which team has a higher chance of spawning.\nThe initial settings (24:18) give around 42.8% chance for the CI to spawn instead of the MTF.\nThe CI spawn chance can be calculated by this formula: CI_TICKETS / (MTF_TICKETS + CI_TICKETS) * 100%")]
        public int RespawnMtfChance { get; set; } = 24;
        public int RespawnCiChance { get; set; } = 18;
        [Description("Quality of Life Settings")]
        public bool SpawnWhileCassieSpeaks { get; set; } = false;
        public bool SpawnWhileNukeIsDetonatonating { get; set; } = false;
        public int CiReplaceGuardsChance { get; set; } = 0;
    }
}
