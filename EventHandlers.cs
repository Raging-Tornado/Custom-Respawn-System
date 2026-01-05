using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.Events.EventArgs.Server;
using MEC;
using PlayerRoles;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YamlDotNet.Core.Tokens;
using Player = Exiled.API.Features.Player;
using Random = UnityEngine.Random;
using Round = Exiled.API.Features.Round;

namespace CustomRespawnSystem
{
    public class EventHandlers
    {
        public static CoroutineHandle RespawnCoroutineHandle;
        public void RespawningTeam(RespawningTeamEventArgs ev)
        {
            ev.IsAllowed = false;
        }
        public void RoundStarted()
        {
            if (Plugin.Instance.Config.PriorityMtfRespawn == true)
            {
                EventHandlers.RespawnCoroutineHandle = Timing.RunCoroutine(RespawnLoopMtfPriority());
            }
            else
            {
                EventHandlers.RespawnCoroutineHandle = Timing.RunCoroutine(RespawnLoopCiPriority());
            }
        }
        public void EndingRound(EndingRoundEventArgs ev)
        {
            Timing.KillCoroutines(RespawnCoroutineHandle);
        }
        private static List<Player> ShufflePlayers(IEnumerable<Player> players)
        {
            return players.OrderBy(_ => UnityEngine.Random.value).ToList();
        }
        public static IEnumerator<float> RespawnLoopMtfPriority()
        {
            while (!Round.IsLobby)
            {
                yield return Timing.WaitForSeconds(Random.Range(Plugin.Instance.Config.MinimumTimeToSpawn, (Plugin.Instance.Config.MaximumTimeToSpawn + 1)));
                float ChosenWave = Random.Range(0, 101);
                float MTF_CHANCE = Plugin.Instance.Config.RespawnMtfChance;
                float CI_CHANCE = Plugin.Instance.Config.RespawnCiChance;
                float ciPercent = (CI_CHANCE / (MTF_CHANCE + CI_CHANCE)) * 100f;
                if (ChosenWave >= ciPercent)
                {
                    int SpawnCount = 0;
                    var deadPlayers = ShufflePlayers(Player.List.Where(x => x.Role.Team == Team.Dead));
                    foreach (Player player in deadPlayers)
                    {
                        if (player is null)
                            continue;

                        if (SpawnCount == 0 && SpawnCount < Plugin.Instance.Config.MaximumMtfRespawnAmount)
                        {
                            player.Role.Set(RoleTypeId.NtfCaptain);
                            SpawnCount++;
                        }
                        else if (SpawnCount >= 1 && SpawnCount <= 3 && SpawnCount < Plugin.Instance.Config.MaximumMtfRespawnAmount)
                        {
                            player.Role.Set(RoleTypeId.NtfSergeant);
                            SpawnCount++;
                        }
                        else if (SpawnCount < Plugin.Instance.Config.MaximumMtfRespawnAmount)
                        {
                            player.Role.Set(RoleTypeId.NtfPrivate);
                            SpawnCount++;
                        }
                    }
                    int SCPAmount = 0;
                    foreach (Player player in Player.List)
                    {
                        if (player is null)
                            continue;

                        if (player.IsScp)
                        {
                            ++SCPAmount;
                        }
                    }
                    if (SCPAmount == 0)
                    {
                        LabApi.Features.Wrappers.Cassie.Message($"MTFUNIT EPSILON 11 DESIGNATED NINETAILEDFOX HASENTERED . ALLREMAINING . NOSCPSLEFT .", $"Mobile Task Force unit, Epsilon-11, designated, Nine-Tailed Fox, has entered the facility.<split>All remaining personnel are advised to proceed with standard evacuation protocols, until an MTF squad reaches your destination.<split>Substantial threat to safety remains within the facility -- exercise caution.");
                    }
                    if (SCPAmount == 1)
                    {
                        LabApi.Features.Wrappers.Cassie.Message($"MTFUNIT EPSILON 11 DESIGNATED NINETAILEDFOX HASENTERED . ALLREMAINING . AWAITINGRECONTAINMENT {SCPAmount} SCPSUBJECT .", $"Mobile Task Force unit, Epsilon-11, designated, Nine-Tailed Fox, has entered the facility.<split>All remaining personnel are advised to proceed with standard evacuation protocols, until an MTF squad reaches your destination.<split>Awaiting recontainment of: {SCPAmount} SCP subject.");
                    }
                    if (SCPAmount > 1)
                    {
                        LabApi.Features.Wrappers.Cassie.Message($"MTFUNIT EPSILON 11 DESIGNATED NINETAILEDFOX HASENTERED . ALLREMAINING . AWAITINGRECONTAINMENT {SCPAmount} SCPSUBJECTS .", $"Mobile Task Force unit, Epsilon-11, designated, Nine-Tailed Fox, has entered the facility.<split>All remaining personnel are advised to proceed with standard evacuation protocols, until an MTF squad reaches your destination.<split>Awaiting recontainment of: {SCPAmount} SCP subjects.");
                    }
                }
                else
                {
                    int SpawnCount = 0;
                    var deadPlayers = ShufflePlayers(Player.List.Where(x => x.Role.Team == Team.Dead));
                    foreach (Player player in deadPlayers)
                    {
                        if (player is null)
                            continue;

                        if (SpawnCount == 0 && SpawnCount < Plugin.Instance.Config.MaximumCiRespawnAmount)
                        {
                            player.Role.Set(RoleTypeId.ChaosMarauder);
                            SpawnCount++;
                        }
                        else if (SpawnCount >= 1 && SpawnCount <= 3 && SpawnCount < Plugin.Instance.Config.MaximumCiRespawnAmount)
                        {
                            player.Role.Set(RoleTypeId.ChaosRepressor);
                            SpawnCount++;
                        }
                        else if (SpawnCount < Plugin.Instance.Config.MaximumCiRespawnAmount)
                        {
                            player.Role.Set(RoleTypeId.ChaosRifleman);
                            SpawnCount++;
                        }
                    }
                    foreach (Player player in Player.List.Where(x => x.Role.Side == Side.ChaosInsurgency))
                    {
                        player.Broadcast(25, "Chaos Insurgency has entered the Facility", Broadcast.BroadcastFlags.Normal, false);
                    }
                }
                yield return Timing.WaitForOneFrame;
            }
        }
        public static IEnumerator<float> RespawnLoopCiPriority()
        {
            while (!Round.IsLobby)
            {
                yield return Timing.WaitForSeconds(Random.Range(Plugin.Instance.Config.MinimumTimeToSpawn, (Plugin.Instance.Config.MaximumTimeToSpawn + 1)));
                float ChosenWave = Random.Range(0, 101);
                float MTF_CHANCE = Plugin.Instance.Config.RespawnMtfChance;
                float CI_CHANCE = Plugin.Instance.Config.RespawnCiChance;
                float ciPercent = (CI_CHANCE / (MTF_CHANCE + CI_CHANCE)) * 100f;
                if (ChosenWave >= ciPercent)
                {
                    int SpawnCount = 0;
                    foreach (Player player in Player.List.Where(x => x.Role.Team == Team.Dead))
                    {
                        if (player is null)
                            continue;

                        if (SpawnCount == 0 && SpawnCount < Plugin.Instance.Config.MaximumCiRespawnAmount)
                        {
                            player.Role.Set(RoleTypeId.ChaosMarauder);
                            SpawnCount++;
                        }
                        else if (SpawnCount >= 1 && SpawnCount <= 3 && SpawnCount < Plugin.Instance.Config.MaximumCiRespawnAmount)
                        {
                            player.Role.Set(RoleTypeId.ChaosRepressor);
                            SpawnCount++;
                        }
                        else if (SpawnCount < Plugin.Instance.Config.MaximumCiRespawnAmount)
                        {
                            player.Role.Set(RoleTypeId.ChaosRifleman);
                            SpawnCount++;
                        }
                    }
                    foreach (Player player in Player.List.Where(x => x.Role.Side == Side.ChaosInsurgency))
                    {
                        player.Broadcast(25, "Chaos Insurgency has entered the Facility", Broadcast.BroadcastFlags.Normal, false);
                    }
                }
                else
                {
                    int SpawnCount = 0;
                    foreach (Player player in Player.List.Where(x => x.Role.Team == Team.Dead))
                    {
                        if (player is null)
                            continue;

                        if (SpawnCount == 0 && SpawnCount < Plugin.Instance.Config.MaximumMtfRespawnAmount)
                        {
                            player.Role.Set(RoleTypeId.NtfCaptain);
                            SpawnCount++;
                        }
                        else if (SpawnCount >= 1 && SpawnCount <= 3 && SpawnCount < Plugin.Instance.Config.MaximumMtfRespawnAmount)
                        {
                            player.Role.Set(RoleTypeId.NtfSergeant);
                            SpawnCount++;
                        }
                        else if (SpawnCount < Plugin.Instance.Config.MaximumMtfRespawnAmount)
                        {
                            player.Role.Set(RoleTypeId.NtfPrivate);
                            SpawnCount++;
                        }
                    }
                    int SCPAmount = 0;
                    foreach (Player player in Player.List)
                    {
                        if (player is null)
                            continue;

                        if (player.IsScp)
                        {
                            ++SCPAmount;
                        }
                    }
                    if (SCPAmount == 0)
                    {
                        LabApi.Features.Wrappers.Cassie.Message($"MTFUNIT EPSILON 11 DESIGNATED NINETAILEDFOX HASENTERED . ALLREMAINING . NOSCPSLEFT .", $"Mobile Task Force unit, Epsilon-11, designated, Nine-Tailed Fox, has entered the facility.<split>All remaining personnel are advised to proceed with standard evacuation protocols, until an MTF squad reaches your destination.<split>Substantial threat to safety remains within the facility -- exercise caution.");
                    }
                    if (SCPAmount == 1)
                    {
                        LabApi.Features.Wrappers.Cassie.Message($"MTFUNIT EPSILON 11 DESIGNATED NINETAILEDFOX HASENTERED . ALLREMAINING . AWAITINGRECONTAINMENT {SCPAmount} SCPSUBJECT .", $"Mobile Task Force unit, Epsilon-11, designated, Nine-Tailed Fox, has entered the facility.<split>All remaining personnel are advised to proceed with standard evacuation protocols, until an MTF squad reaches your destination.<split>Awaiting recontainment of: {SCPAmount} SCP subject.");
                    }
                    if (SCPAmount > 1)
                    {
                        LabApi.Features.Wrappers.Cassie.Message($"MTFUNIT EPSILON 11 DESIGNATED NINETAILEDFOX HASENTERED . ALLREMAINING . AWAITINGRECONTAINMENT {SCPAmount} SCPSUBJECTS .", $"Mobile Task Force unit, Epsilon-11, designated, Nine-Tailed Fox, has entered the facility.<split>All remaining personnel are advised to proceed with standard evacuation protocols, until an MTF squad reaches your destination.<split>Awaiting recontainment of: {SCPAmount} SCP subjects.");
                    }
                }
                yield return Timing.WaitForOneFrame;
            }
        }
    }
}
