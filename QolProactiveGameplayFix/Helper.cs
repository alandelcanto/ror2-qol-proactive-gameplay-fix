using System;
using System.Collections.Generic;
using System.Text;
using RoR2;
using UnityEngine.UIElements;
using UnityEngine;
using UnityEngine.Networking;

namespace QolProactiveGameplayFix
{
    internal class Helper
    {
        public static PickupIndex PickRandomTier2Item()
        {
            return Run.instance.availableTier2DropList[Run.instance.treasureRng.RangeInt(0, Run.instance.availableTier2DropList.Count)];
        }

        public static PickupIndex PickRandomBossItem()
        {
            return Run.instance.availableBossDropList[Run.instance.treasureRng.RangeInt(0, Run.instance.availableBossDropList.Count)];
        }

        public static void DebugSpawnBoss(Vector3 position, string masterPrefabName = "MagmaWormMaster", TeamIndex team = TeamIndex.Monster)
        {
            if (!NetworkServer.active)
            {
                return;
            }

            GameObject masterPrefab = MasterCatalog.FindMasterPrefab(masterPrefabName);
            if (masterPrefab == null)
            {
                Log.Error($"master prefab not found: {masterPrefabName}");
                return;
            }

            var spawnRequest = new MasterSummon
            {
                masterPrefab = masterPrefab,
                position = position,
                rotation = Quaternion.identity,
                summonerBodyObject = null,
                ignoreTeamMemberLimit = true,
                teamIndexOverride = team
            }.Perform();
        }
    }
}
