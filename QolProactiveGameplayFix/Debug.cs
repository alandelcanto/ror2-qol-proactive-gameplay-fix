using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using RoR2;
using UnityEngine.Networking;

namespace QolProactiveGameplayFix
{
    internal class DebugComponent : NetworkBehaviour
    {
        public static void Init()
        {
            On.RoR2.CharacterBody.Start += (orig, self) =>
            {
                orig(self);

                if (self.isPlayerControlled && self.GetComponent<DebugComponent>() == null)
                {
                    self.gameObject.AddComponent<DebugComponent>();
                }
            };
        }
        private void Update()
        {
            if (!hasAuthority) { return; }

            if (Input.GetKeyDown(KeyCode.F2))
            {
                CmdSpawnItems(GetCharacterTransform());
            }

            if (Input.GetKeyDown(KeyCode.F3))
            {
                CmdSpawnBoss(GetCharacterTransform());
            }
        }

        private Transform GetCharacterTransform()
        {
            return GetComponent<CharacterBody>().transform;
        }

        [Command]
        private static void CmdSpawnItems(Transform transform)
        {
            // Drop all replaced items
            Log.Info($"Player pressed F2. Spawning our custom items at coordinates {transform.position}");
            PickupDropletController.CreatePickupDroplet(new UniquePickup(PickupCatalog.FindPickupIndex(TrophyHunterFix.equipmentDef.equipmentIndex)), transform.position, transform.forward * 20f, false);
        }

        [Command]
        private static void CmdSpawnBoss(Transform transform)
        {
            Log.Info($"Player pressed F3. Spawning a boss");
            if (Run.instance.treasureRng.RangeInt(0, 100) < 10 && QolProactiveGameplayFix.easterEggsActive)
            {
                // funni
                for (int i = 0; i < 10; i++)
                {
                    Helper.DebugSpawnBoss(transform.position + transform.forward * (15f + 3f*i), "BrotherMaster");
                    if (i % 2 == 0)
                    {
                        Helper.DebugSpawnBoss(transform.position + transform.forward * (15f + 3f * i), "TitanGoldMaster", TeamIndex.Player);
                    }
                }
            } else
            {
                // Spawn a boss
                
                Helper.DebugSpawnBoss(transform.position + transform.forward * 15f);
            }

            
        }
       
    }
}
