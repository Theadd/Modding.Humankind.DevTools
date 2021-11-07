using System;
using BepInEx.Configuration;
using UnityEngine;

namespace Modding.Humankind.DevTools
{
    /// <summary>
    ///     Binds an static method as the action to invoke once the specified <c>KeyCode</c>s are pressed.
    /// </summary>
    /// <remarks>The method's class must be annotated with [DevToolsModule] attribute, otherwise it will be ignored.</remarks>
    /// <remarks>These shortcuts will be available in-game only. Not in main menu, neither when game is still loading.</remarks>
    /// <remarks>It is a simplified version of BepInEx's <c>KeyboardShortcut</c> which ensures that the action will be triggered only once each time it is pressed.</remarks>
    [AttributeUsage(AttributeTargets.Method)]
    public class InGameKeyboardShortcutAttribute : Attribute
    {
        /// <summary>
        ///     Binds an static method as the action to invoke once the specified <c>KeyCode</c>s are pressed.
        /// </summary>
        /// <param name="actionName">Unique string representing this keyboard shortcut.</param>
        /// <param name="mainKey">UnityEngine.KeyCode</param>
        /// <param name="modifierKeys">UnityEngine.KeyCode representing modifier keys.</param>
        public InGameKeyboardShortcutAttribute(string actionName, KeyCode mainKey, params KeyCode[] modifierKeys)
        {
            Key = new KeyboardShortcut(mainKey, modifierKeys);
            ActionName = actionName;
        }

        public KeyboardShortcut Key { get; }
        
        public string ActionName { get; }
    }
}
