using System;
using System.Collections.Generic;
using System.Text;
using RoR2;
using UnityEngine.AddressableAssets;
using UnityEngine;
using static RoR2.GenericPickupController;

namespace QolProactiveGameplayFix
{
    internal static class TrophyHunterFix
    {
        // Set when tricorn is fired
        public static bool isTrophyHunterFiring = false;
        // Tricorn item info
        public static EquipmentDef equipmentDef = Addressables.LoadAssetAsync<EquipmentDef>("RoR2/DLC1/BossHunter/BossHunter.asset").WaitForCompletion();

        public static void Init()
        {
            Log.Debug($"{QolProactiveGameplayFix.PluginName}: Adding fix for:");

            // Hooks
            On.RoR2.EquipmentSlot.PerformEquipmentAction += EquipmentSlot_PerformEquipmentAction;
            On.RoR2.PickupDropletController.CreatePickupDroplet_CreatePickupInfo_Vector3_Vector3 += PickupDropletController_CreatePickupDroplet_CreatePickupInfo_Vector3_Vector3;
        }

        // Item spawn after killing boss hook
        private static void PickupDropletController_CreatePickupDroplet_CreatePickupInfo_Vector3_Vector3(On.RoR2.PickupDropletController.orig_CreatePickupDroplet_CreatePickupInfo_Vector3_Vector3 orig, GenericPickupController.CreatePickupInfo pickupInfo, Vector3 position, Vector3 velocity)
        {
            // If source is tricorn
            if (isTrophyHunterFiring)
            {
                PickupIndex pickedItem;

                    // A random Boss Item instead of normal
                    pickedItem = Helper.PickRandomBossItem();

                pickupInfo._pickupState = pickupInfo._pickupState.Value.WithPickupIndex(pickedItem);
            }

            orig(pickupInfo, position, velocity);
        }

        

        private static bool EquipmentSlot_PerformEquipmentAction(On.RoR2.EquipmentSlot.orig_PerformEquipmentAction orig, EquipmentSlot self, EquipmentDef equipmentDef)
        {
            // Compare with Tricorn info
            if (equipmentDef.equipmentIndex == TrophyHunterFix.equipmentDef.equipmentIndex)
            {
                isTrophyHunterFiring = true;
                bool result = orig(self, equipmentDef);
                isTrophyHunterFiring = false;
                return result;
            }
            return orig(self, equipmentDef);
        }
    }
}
