﻿using System;
using System.Collections.Generic;
using Amplitude.Mercury.Sandbox;

namespace Modding.Humankind.DevTools.Core
{
    internal abstract class GameEventController
    {
        internal static event Action OnNewTurnStarts;
        internal static event Action OnGameHasLoaded;
        internal static event Action OnGameHasUnloaded;
        
        private static List<Action> _onNewTurnActions = new List<Action>();

        internal static void AddOnNewTurnAction(Action action)
        {
            OnNewTurnStarts += action;
            _onNewTurnActions.Add(action);
        }
        
        internal static void RemoveOnNewTurnAction(Action action)
        {
            OnNewTurnStarts -= action;
            _onNewTurnActions.Remove(action);
        }
        
        protected static void RemoveAllOnNewTurnActions()
        {
            foreach (Action action in _onNewTurnActions)
            {
                OnNewTurnStarts -= action;
            }
            _onNewTurnActions.Clear();
            AddOnNewTurnAction(Dummy);
        }

        private static void Dummy() { }

        protected static void InvokeOnGameHasLoaded()
        {
            if (!BepInEx.Utility.TryDo(OnGameHasLoaded, out Exception ex))
                Loggr.Log(ex);
        }
        
        protected static void InvokeOnNewTurnStart()
        {
            if (!BepInEx.Utility.TryDo(OnNewTurnStarts, out Exception ex))
                Loggr.Log(ex);
        }
        
        protected static void InvokeOnGameHasUnloaded()
        {
            if (!BepInEx.Utility.TryDo(OnGameHasUnloaded, out Exception ex))
                Loggr.Log(ex);

            RemoveAllOnNewTurnActions();

            OnGameHasLoaded = null;
            OnNewTurnStarts = null;
            OnGameHasUnloaded = null;
        }

        internal static void ReloadAllModules(bool invokeOnGameHasLoaded)
        {
            if (!SandboxManager.IsStarted)
            {
                Loggr.LogError("SandboxManager is NOT started IN ReloadAllModules.");
                return;
            }            
            if (!BepInEx.Utility.TryDo(OnGameHasUnloaded, out Exception ex))
            {
                // Ignore exceptions while reloading all modules
            }

            RemoveAllOnNewTurnActions();
            
            if (!DevTools.QuietMode)
                Loggr.Debug("Reloading modules...");
                
            ModuleHelper.FindAllAndRegister();
            
            if (invokeOnGameHasLoaded)
                InvokeOnGameHasLoaded();
        }
    }
}
