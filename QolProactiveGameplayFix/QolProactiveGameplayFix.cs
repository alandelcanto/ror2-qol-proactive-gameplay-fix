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

        public const bool isDebugActive = true;
        public const bool easterEggsActive = true;


        public void Awake()
        {
            Log.Init(Logger);
            if (isDebugActive )
            {
                DebugComponent.Init();
            }
            TrophyHunterFix.Init();
        }
    }
}
