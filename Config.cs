using Exiled.API.Interfaces;
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
        //public string MtfAnnouncementNoSCPs { get; set; } = "MTFUNIT EPSILON 11 DESIGNATED NINETAILEDFOX HASENTERED . ALLREMAINING . NOSCPSLEFT .";
        //public string MtfAnnouncementSubtitleNoSCPs { get; set; } = "Mobile Task Force unit, Epsilon-11, designated, Nine-Tailed Fox, has entered the facility.<split>All remaining personnel are advised to proceed with standard evacuation protocols, until an MTF squad reaches your destination.<split>Substantial threat to safety remains within the facility -- exercise caution.";
        //public string MtfAnnouncementOneSCP { get; set; } = "$MTFUNIT EPSILON 11 DESIGNATED NINETAILEDFOX HASENTERED . ALLREMAINING . AWAITINGRECONTAINMENT {SCPAmount} SCPSUBJECT .";
        //public string MtfAnnouncementSubtitleOneSCP { get; set; } = "$Mobile Task Force unit, Epsilon-11, designated, Nine-Tailed Fox, has entered the facility.<split>All remaining personnel are advised to proceed with standard evacuation protocols, until an MTF squad reaches your destination.<split>Awaiting recontainment of: {SCPAmount} SCP subject.";
        //public string MtfAnnouncementMultipleSCPs { get; set; } = "$MTFUNIT EPSILON 11 DESIGNATED NINETAILEDFOX HASENTERED . ALLREMAINING . AWAITINGRECONTAINMENT {SCPAmount} SCPSUBJECTS .";
        //public string MtfAnnouncementSubtitleMultipleSCPs { get; set; } = "$Mobile Task Force unit, Epsilon-11, designated, Nine-Tailed Fox, has entered the facility.<split>All remaining personnel are advised to proceed with standard evacuation protocols, until an MTF squad reaches your destination.<split>Awaiting recontainment of: {SCPAmount} SCP subjects.";
    }
}
