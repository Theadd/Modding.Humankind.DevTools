using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using Modding.Humankind.DevTools.Core;

namespace Modding.Humankind.DevTools
{

    [BepInPlugin(PLUGIN_GUID, "DevTools", "1.3.0.0")]
    [BepInIncompatibility("AOM.Humankind.Teams")]
    internal class DevTools : BaseUnityPlugin
    {
        const string PLUGIN_GUID = "Modding.Humankind.DevTools";

        internal static ManualLogSource Log;

        public static bool IncludeExternalModules = true;

        private Harmony _harmony;

        private void Awake()
        {
            Log = Logger;
            
            _harmony = new Harmony(PLUGIN_GUID);

            _harmony.PatchAll();
        }

        private void FixedUpdate()
        {
            ActionManager.Run();
        }

        private void OnDestroy()
        {
            _harmony?.UnpatchSelf();
            GameController.Unload();
            ActionManager.Unload();
            Loggr.Debug("Unloaded DevTools Framework.");
        }
    }
}
