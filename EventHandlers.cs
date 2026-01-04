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
                    foreach (Player player in Player.List.Where(x => x.Role.Team == Team.Dead))
                    {
                        if (player is null)
                            continue;

                        if (SpawnCount == 0)
                        {
                            player.Role.Set(RoleTypeId.NtfCaptain);
                            SpawnCount++;
                        }
                        else if (SpawnCount >= 1 && SpawnCount <= 3)
                        {
                            player.Role.Set(RoleTypeId.NtfSergeant);
                            SpawnCount++;
                        }
                        else if (SpawnCount < 15)
                        {
                            player.Role.Set(RoleTypeId.NtfPrivate);
                            SpawnCount++;
                        }
                    }
                }
                else
                {
                    int SpawnCount = 0;
                    foreach (Player player in Player.List.Where(x => x.Role.Team == Team.Dead))
                    {
                        if (player is null)
                            continue;

                        if (SpawnCount == 0)
                        {
                            player.Role.Set(RoleTypeId.ChaosMarauder);
                            SpawnCount++;
                        }
                        else if (SpawnCount >= 1 && SpawnCount <= 3)
                        {
                            player.Role.Set(RoleTypeId.ChaosRepressor);
                            SpawnCount++;
                        }
                        else if (SpawnCount < 15)
                        {
                            player.Role.Set(RoleTypeId.ChaosRifleman);
                            SpawnCount++;
                        }
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

                        if (SpawnCount == 0)
                        {
                            player.Role.Set(RoleTypeId.ChaosMarauder);
                            SpawnCount++;
                        }
                        else if (SpawnCount >= 1 && SpawnCount <= 3)
                        {
                            player.Role.Set(RoleTypeId.ChaosRepressor);
                            SpawnCount++;
                        }
                        else if (SpawnCount < 15)
                        {
                            player.Role.Set(RoleTypeId.ChaosRifleman);
                            SpawnCount++;
                        }
                    }
                }
                else
                {
                    int SpawnCount = 0;
                    foreach (Player player in Player.List.Where(x => x.Role.Team == Team.Dead))
                    {
                        if (player is null)
                            continue;

                        if (SpawnCount == 0)
                        {
                            player.Role.Set(RoleTypeId.NtfCaptain);
                            SpawnCount++;
                        }
                        else if (SpawnCount >= 1 && SpawnCount <= 3)
                        {
                            player.Role.Set(RoleTypeId.NtfSergeant);
                            SpawnCount++;
                        }
                        else if (SpawnCount < 15)
                        {
                            player.Role.Set(RoleTypeId.NtfPrivate);
                            SpawnCount++;
                        }
                    }
                }
                yield return Timing.WaitForOneFrame;
            }
        }
    }
}
