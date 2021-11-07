using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using Modding.Humankind.DevTools.Core;

namespace Modding.Humankind.DevTools
{

    [BepInPlugin(PLUGIN_GUID, "DevTools", "1.2.0.0")]
    [BepInIncompatibility("AOM.Humankind.Teams")]
    public class DevTools : BaseUnityPlugin
    {
        const string PLUGIN_GUID = "Modding.Humankind.DevTools";

        internal static ManualLogSource Log;

        public static bool IncludeExternalModules = true;

        public static DevTools Instance;

        private Harmony _harmony;

        private void Awake()
        {
            Instance = this;
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
            Instance = null;
        }
    }
}
