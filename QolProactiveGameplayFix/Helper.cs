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

        public static void DebugSpawnBoss(string masterPrefabName, Vector3 position)
        {
            if (!NetworkServer.active)
            {
                return;
            }

            GameObject masterPrefab = MasterCatalog.FindMasterPrefab(masterPrefabName);
            if (masterPrefab == null)
            {
                Debug.LogError($"master prefab not found: {masterPrefabName}");
                return;
            }

            var spawnRequest = new MasterSummon
            {
                masterPrefab = masterPrefab,
                position = position,
                rotation = Quaternion.identity,
                summonerBodyObject = null,
                ignoreTeamMemberLimit = true,
                teamIndexOverride = TeamIndex.Monster
            }.Perform();
        }
    }
}
