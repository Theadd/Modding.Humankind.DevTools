using System;
using System.Collections.Generic;

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
            {
                Loggr.LogError(ex.ToString());
            }
        }
        
        protected static void InvokeOnNewTurnStart()
        {
            if (!BepInEx.Utility.TryDo(OnNewTurnStarts, out Exception ex))
            {
                Loggr.LogError(ex.ToString());
                Loggr.LogError(ex.StackTrace);
            }
        }
        
        protected static void InvokeOnGameHasUnloaded()
        {
            if (!BepInEx.Utility.TryDo(OnGameHasUnloaded, out Exception ex))
            {
                Loggr.LogError(ex.ToString());
            }

            RemoveAllOnNewTurnActions();

            OnGameHasLoaded = null;
            OnNewTurnStarts = null;
            OnGameHasUnloaded = null;
        }
    }
}
