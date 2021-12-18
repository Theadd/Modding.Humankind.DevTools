using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using Modding.Humankind.DevTools.Core;
using Modding.Humankind.DevTools.DeveloperTools;
using UnityEngine;

namespace Modding.Humankind.DevTools
{

    [BepInPlugin(PLUGIN_GUID, "DevTools", "1.4.0.0")]
    [BepInIncompatibility("AOM.Humankind.Teams")]
    public class DevTools : BaseUnityPlugin
    {
        const string PLUGIN_GUID = "Modding.Humankind.DevTools";

        internal static ManualLogSource Log;

        public static bool IncludeExternalModules = true;

        public static DevTools Instance { get; private set; }

        private Harmony _harmony;

        private static AssetLoader _assets;

        public static AssetLoader Assets =>
            _assets ?? (_assets = new AssetLoader()
            {
                Assembly = typeof(DevTools).Assembly,
                ManifestResourceName = PLUGIN_GUID + ".Resources.resources"
            });

        private void Awake()
        {
            Log = Logger;
            Instance = this;
            
            _harmony = new Harmony(PLUGIN_GUID);

            _harmony.PatchAll();
        }

        private void FixedUpdate()
        {
            ActionManager.Run();
        }

        void OnGUI () {
            // -
        }

        public static GameObject GetGameObject()
        {
            return Instance?.gameObject;
        }

        private void OnDestroy()
        {
            _harmony?.UnpatchSelf();
            GameController.Unload();
            ActionManager.Unload();
            Assets.Unload(true);
            Loggr.Debug("Unloaded DevTools Framework.");
        }
    }
}
