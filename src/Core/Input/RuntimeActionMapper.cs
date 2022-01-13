using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BepInEx.Configuration;

namespace Modding.Humankind.DevTools.Core
{
    public static class RuntimeActionMapper
    {
        private static Dictionary<KeyboardShortcut, string> runtimeActionsMap =
            new Dictionary<KeyboardShortcut, string>();

        private static Dictionary<string, MethodInfo> runtimeMethodsMap = 
            new Dictionary<string, MethodInfo>();

        public static bool GetFirstShortcutMatching(out string actionName, out KeyboardShortcut shortkey)
        {
            if (ActionManager._mappedActions != null)
            {
                foreach (var map in ActionManager._mappedActions
                    .Where(map => map.Key.IsDown()))
                {
                    shortkey = map.Key;
                    actionName = map.Value;

                    return true;
                }
            }
            foreach (var map in runtimeActionsMap.Where(map => map.Key.IsDown()))
            {
                shortkey = map.Key;
                actionName = map.Value;

                return true;
            }

            shortkey = KeyboardShortcut.Empty;
            actionName = null;

            return false;
        }
        
        public static void InvokeAction(string actionName)
        {
            if (ActionManager._mappedMethods != null && ActionManager._mappedMethods.TryGetValue(actionName, out var method))
            {
                method.Invoke(null, null);
                
                if (!DevTools.QuietMode)
                    Loggr.Announce("Action " + actionName + " executed.");
            }
            else
            {
                if (runtimeMethodsMap.TryGetValue(actionName, out var runtimeMethod))
                {
                    try
                    {
                        runtimeMethod.Invoke(null, null);
                        
                        if (!DevTools.QuietMode)
                            Loggr.Announce("Action " + actionName + " executed.");
                    }
                    catch (Exception) { /* Ignore exceptions in runtime actions */ }
                }
                else
                {
                    if (!DevTools.QuietMode)
                        Loggr.LogError("Action " + actionName + " NOT Found.");
                }
            }
        }
        
        public static bool RegisterRuntimeAction(KeyboardShortcut key, string actionName, Action action)
        {
            return RegisterRuntimeAction(key, actionName, action.GetMethodInfo());
        }
        
        private static bool RegisterRuntimeAction(KeyboardShortcut key, string actionName, MethodInfo staticMethodInfo)
        {
            if (actionName == "")
                return false;

            if (ActionManager._mappedActions != null && ActionManager._mappedActions.ContainsKey(key))
            {
                Loggr.LogWarning("Unable to register key [" + key + "] since it's already registered by " + 
                                 "a [DevToolsModule] using the [InGameKeyboardShortcut] attribute.");
                return false;
            }
            if (ActionManager._mappedMethods != null && ActionManager._mappedMethods.ContainsKey(actionName))
            {
                Loggr.LogWarning("Unable to register another action as '" + key + "' since it's already registered by " + 
                                 "a [DevToolsModule] using the [InGameKeyboardShortcut] attribute.");
                return false;
            }

            if (runtimeActionsMap.ContainsKey(key))
            {
                if (runtimeActionsMap[key] != actionName)
                {
                    Loggr.LogWarning("Unable to register key [" + key +
                                   "] since it's already registered to a different action name.");
                    return false;
                }

                runtimeMethodsMap[actionName] = staticMethodInfo;
            }
            else
            {
                if (runtimeMethodsMap.ContainsKey(actionName))
                {
                    var prevKey = runtimeActionsMap.FirstOrDefault(x => x.Value == actionName).Key;
                    runtimeActionsMap.Remove(prevKey);
                    runtimeActionsMap.Add(key, actionName);
                    runtimeMethodsMap[actionName] = staticMethodInfo;

                }
                else
                {
                    runtimeActionsMap.Add(key, actionName);
                    runtimeMethodsMap.Add(actionName, staticMethodInfo);
                }
            }

            if (!DevTools.QuietMode)
                Loggr.Log("\t%Green%[" + key + "]%Gray% => %Default%" + actionName, ConsoleColor.White);

            return true;
        }
    }
}
