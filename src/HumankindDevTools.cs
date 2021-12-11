using System;
using System.Collections.Generic;
using Modding.Humankind.DevTools.Core;

namespace Modding.Humankind.DevTools
{
    /// <summary>
    ///     Development related tools.
    /// </summary>
    public static class HumankindDevTools
    {
        /// <summary>
        ///     Reloads all registered modules.
        /// </summary>
        public static void ReloadAllModules() => GameController.ReloadAllModules(true);

        /// <summary>
        ///     Unregisters all members of a class Type after invoking any method with `[OnGameHasUnloaded]` attribute.
        /// </summary>
        /// <param name="moduleType"></param>
        public static void UnloadModule(Type moduleType)
        {
            if (HumankindGame.IsGameLoaded)
                ModuleHelper.UnloadModule(moduleType);
        }

        /// <summary>
        ///     Registers all members of a class Type with DevTool's attribute annotations.
        /// </summary>
        /// <param name="moduleType">A class Type with members annotated with DevTools attributes.</param>
        public static void LoadModule(Type moduleType)
        {
            if (HumankindGame.IsGameLoaded)
                ModuleHelper.LoadModule(moduleType);
        }
        
        /// <summary>
        ///     
        /// </summary>
        public static event Action OnIterateNext
        {
            add => AddOnIterateNextAction(value);
            remove => RemoveOnIterateNextAction(value);
        }

        internal static void IterateNextAction()
        {
            if (!BepInEx.Utility.TryDo(onIterateNextAction, out Exception ex))
            {
                Loggr.LogError(ex.ToString());
            }
        }
        
        internal static event Action onIterateNextAction;
        
        private static List<Action> _onIterateNextActions = new List<Action>();

        internal static void AddOnIterateNextAction(Action action)
        {
            onIterateNextAction += action;
            _onIterateNextActions.Add(action);
        }
        
        internal static void RemoveOnIterateNextAction(Action action)
        {
            onIterateNextAction -= action;
            _onIterateNextActions.Remove(action);
        }
        
        internal static void RemoveAllOnIterateNextAction()
        {
            foreach (Action action in _onIterateNextActions)
            {
                onIterateNextAction -= action;
            }
            _onIterateNextActions.Clear();
        }
    }
}
