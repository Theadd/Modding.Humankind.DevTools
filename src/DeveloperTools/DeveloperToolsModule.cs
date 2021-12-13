﻿using System;
using UnityEngine;

namespace Modding.Humankind.DevTools.Core
{
    [DevToolsModule]
    public class DeveloperToolsModule
    {
        public static string Name => "DeveloperTools";

        [OnGameHasLoaded]
        [OnGameHasUnloaded]
        private static void Reset()
        {
            HumankindDevTools.RemoveAllOnIterateNextAction();
            HumankindDevTools.AddOnIterateNextAction(Dummy);
        }
        
        private static void Dummy() { }
        
        [InGameKeyboardShortcut("Reload all modules", KeyCode.R, KeyCode.LeftControl, KeyCode.LeftAlt, KeyCode.LeftShift)]
        public static void ReloadAllModules()
        {
            Loggr.Log("Reloading all modules...", ConsoleColor.Magenta);
            HumankindDevTools.ReloadAllModules();
        }
        
        [InGameKeyboardShortcut("Print game statistics", KeyCode.P, KeyCode.LeftControl)]
        public static void PrintGameStatistics()
        {
            Loggr.Log(HumankindGame.ToString(), ConsoleColor.DarkCyan);
        }
        
        [InGameKeyboardShortcut("Updates game's UI to reflect all changes", KeyCode.U, KeyCode.LeftAlt)]
        public static void UpdateGameUI()
        {
            HumankindGame.Update();
        }
        
        [InGameKeyboardShortcut("Iterate Next Action", KeyCode.F3)]
        public static void IterateNextAction()
        {
            HumankindDevTools.IterateNextAction();
        }
        
        [InGameKeyboardShortcut("Remove all OnIterateNext registered actions", KeyCode.F3, KeyCode.LeftControl, KeyCode.LeftShift)]
        public static void RemoveAllOnIterateNextActions()
        {
            Reset();
        }
    }
}