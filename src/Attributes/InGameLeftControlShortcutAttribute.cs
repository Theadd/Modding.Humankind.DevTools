using System;
using UnityEngine;

namespace Modding.Humankind.DevTools
{
    /// <summary>
    ///     Binds an static method as the action to invoke once the specified <c>KeyCode</c> is pressed in combination with <c>KeyCode.LeftControl</c>.
    /// </summary>
    /// <remarks>The method's class must be annotated with [DevToolsModule] attribute, otherwise it will be ignored.</remarks>
    /// <remarks>These shortcuts will be available in-game only. Not in main menu, neither when game is still loading.</remarks>
    /// <remarks>It is a simplified version of BepInEx's <c>KeyboardShortcut</c> which ensures that the action will be triggered only once each time it is pressed.</remarks>
    [AttributeUsage(AttributeTargets.Method)]
    public class InGameLeftControlShortcutAttribute : Attribute
    {
        /// <summary>
        ///     Binds an static method as the action to invoke once the specified <c>KeyCode</c> is pressed in combination with <c>KeyCode.LeftControl</c>.
        /// </summary>
        /// <param name="key">UnityEngine.KeyCode</param>
        /// <param name="actionName">Unique string representing this keyboard shortcut</param>
        public InGameLeftControlShortcutAttribute(KeyCode key, string actionName)
        {
            Key = key;
            ActionName = actionName;
        }

        public KeyCode Key { get; }
        
        public string ActionName { get; }
    }
}
