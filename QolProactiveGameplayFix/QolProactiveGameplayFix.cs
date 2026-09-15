using BepInEx;
using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace QolProactiveGameplayFix
{
    [BepInDependency(ItemAPI.PluginGUID)]
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]

    public class QolProactiveGameplayFix : BaseUnityPlugin
    {
        public const string PluginGUID = PluginAuthor + "." + PluginName;
        public const string PluginAuthor = "AuthorName";
        public const string PluginName = "QolProactiveGameplayFix";
        public const string PluginVersion = "1.0.0";


        public void Awake()
        {
            Log.Init(Logger);
            TrophyHunterFix.Init();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F2))
            {
                // Get the player body to use a position:
                var transform = PlayerCharacterMasterController.instances[0].master.GetBodyObject().transform;

                // Drop all replaced items
                Log.Info($"Player pressed F2. Spawning our custom items at coordinates {transform.position}");
                PickupDropletController.CreatePickupDroplet(new UniquePickup(PickupCatalog.FindPickupIndex(TrophyHunterFix.equipmentDef.equipmentIndex)), transform.position, transform.forward * 20f, false);
            }

            if (Input.GetKeyDown(KeyCode.F3))
            {
                // Get the player body to use a position:
                var transform = PlayerCharacterMasterController.instances[0].master.GetBodyObject().transform;

                // Spawn a boss
                Log.Info($"Player pressed F3. Spawning a boss");
                Helper.DebugSpawnBoss("MagmaWormMaster", transform.position + transform.forward * 15f);
            }
        }
    }
}
