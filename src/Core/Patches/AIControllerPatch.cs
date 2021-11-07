using System;
using Amplitude.Mercury.AI;
using Amplitude.Mercury.Sandbox;
using HarmonyLib;
using Modding.Humankind.DevTools.Core;

namespace Modding.Humankind.DevTools
{
    [HarmonyPatch(typeof(AIController))]
    internal class AIControllerPatch
    {
        private static bool _hasSkippedNewGameFirstTurn;

        [HarmonyPatch(nameof(InitializeOnLoad), MethodType.Normal)]
        [HarmonyPostfix]
        public static void InitializeOnLoad(AIController __instance)
        {
            GameController.IsNewGame = false;
            GameController.MakeDirty();
        }

        [HarmonyPatch(nameof(InitializeOnStart), MethodType.Normal)]
        [HarmonyPostfix]
        public static void InitializeOnStart(AIController __instance, SandboxStartSettings sandboxStartSettings)
        {
            GameController.IsNewGame = true;
            _hasSkippedNewGameFirstTurn = false;
        }

        [HarmonyPatch(nameof(OnNewTurnStarted), MethodType.Normal)]
        [HarmonyPrefix]
        public static bool OnNewTurnStarted(AIController __instance)
        {
            if (!_hasSkippedNewGameFirstTurn)
            {
                _hasSkippedNewGameFirstTurn = true;
                if (GameController.IsNewGame) return true;
            }

            if (__instance != Sandbox.AIController) Loggr.LogError(" ERROR! __instance != Sandbox.AIController");
            GameController.Instance.Setup((int) R.Fields.LastRunTurnField.GetValue(__instance), __instance,
                Sandbox.GameID, false);
            Loggr.Log("Setup was triggered from AIController.OnNewTurnStarted()", ConsoleColor.Red);

            return true;
        }
    }
}