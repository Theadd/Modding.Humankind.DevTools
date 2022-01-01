using System;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using Modding.Humankind.DevTools.Core;
using Modding.Humankind.DevTools.DeveloperTools;
using Modding.Humankind.DevTools.DeveloperTools.UI;
using UnityEngine;

namespace Modding.Humankind.DevTools
{

    [BepInPlugin(PLUGIN_GUID, "DevTools", "1.5.0.0")]
    [BepInIncompatibility("AOM.Humankind.Teams")]
    public class DevTools : BaseUnityPlugin
    {
        const string PLUGIN_GUID = "Modding.Humankind.DevTools";

        internal static ManualLogSource Log;

        public static bool IncludeExternalModules = true;

        public static DevTools Instance { get; private set; }

        private Harmony _harmony;

        private static AssetLoader _assets;

        private static bool _uiReady = false;

        public static AssetLoader Assets =>
            _assets ?? (_assets = new AssetLoader()
            {
                Assembly = typeof(DevTools).Assembly,
                ManifestResourceName = PLUGIN_GUID + ".Resources.devtools-resources"
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
            if (!_uiReady) TryToInitializeUI();
        }

        private void Start()
        {
            Loggr.Log("STARTING HUMANKIND DEVTOOLS...", ConsoleColor.Green);
            // UIManager.Initialize();
        }

        void OnGUI () {
            // -
        }

        private void TryToInitializeUI()
        {
            if (GameObject.Find("/WindowsRoot/SystemOverlays") != null)
            {
                _uiReady = true;
                UIManager.Initialize();
                Loggr.Log("HUMANKIND DEVTOOLS UI LOADED SUCCESSFULLY.", ConsoleColor.Green);
            }
        }

        public static GameObject GetGameObject() => Instance?.gameObject;

        private void OnDestroy()
        {
            _harmony?.UnpatchSelf();
            GameController.Unload();
            ActionManager.Unload();
            Assets.Unload(true);
            Destroy(gameObject);
            Loggr.Debug("Unloaded DevTools Framework.");
        }
    }
}
