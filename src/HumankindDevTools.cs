using System;
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
    }
}
