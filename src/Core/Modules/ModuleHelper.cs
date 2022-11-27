using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;

namespace Modding.Humankind.DevTools.Core
{
    internal static class ModuleHelper
    {
        private static List<MethodInfo> _onGameHasLoadedMethods;
        private static List<MethodInfo> _onGameHasUnloadedMethods;

        private static void Initialize()
        {
            Reset();

            GameController.OnGameHasLoaded -= ExecuteOnGameHasLoadedMethods;
            GameController.OnGameHasLoaded += ExecuteOnGameHasLoadedMethods;
            GameController.OnGameHasUnloaded -= ExecuteOnGameHasUnloadedMethods;
            GameController.OnGameHasUnloaded += ExecuteOnGameHasUnloadedMethods;
        }

        internal static void Reset()
        {
            _onGameHasLoadedMethods = new List<MethodInfo>();
            _onGameHasUnloadedMethods = new List<MethodInfo>();

            ActionManager.Reset();
        }

        public static void FindAllAndRegister()
        {
            Initialize();
            
            // Loggr.Log("The following actions will be available while [Listening to Keyboard Shortcuts] is enabled, use [LeftControl + K] to enable/disable it.", ConsoleColor.DarkGreen);

            if (DevTools.IncludeExternalModules)
            {
                try
                {
                    var allAIStrategyModules =
                        from assembly in AppDomain.CurrentDomain.GetAssemblies()    // .AsParallel()
                        from type in assembly.GetTypes()
                        where type.IsDefined(typeof(DevToolsModuleAttribute), false)
                        select type;

                    // allAIStrategyModules.ForAll(RegisterModule);
                    foreach (var aiStrategyModule in allAIStrategyModules) RegisterModule(aiStrategyModule);
                }
                catch (ReflectionTypeLoadException e)
                {
                    if (!DevTools.QuietMode)
                        Loggr.Log(e);
                }
                catch (NullReferenceException ex)
                {
                    if (!DevTools.QuietMode)
                        Loggr.Log(ex);
                }
                catch (Exception otherEx)
                {
                    if (!DevTools.QuietMode)
                        Loggr.Log(otherEx);
                }
            }
            else
            {
                try
                {
                    var ownAIStrategyModules =
                        from type in Assembly.GetExecutingAssembly().GetTypes()
                        where type.IsDefined(typeof(DevToolsModuleAttribute), false)
                        select type;

                    foreach (var aiStrategyModule in ownAIStrategyModules) RegisterModule(aiStrategyModule);
                }
                catch (Exception exc)
                {
                    if (!DevTools.QuietMode)
                        Loggr.Log(exc);
                }
            }
        }

        public static void LoadModule(Type moduleType)
        {
            if (!DevTools.QuietMode)
                Log("Loading " + GetDevToolsModuleName(moduleType) + " DevTool's module...");
            
            RegisterModule(moduleType);
        }

        public static void UnloadModule(Type moduleType)
        {
            var moduleName = GetDevToolsModuleName(moduleType);
            var methodsToRemove = new List<MethodInfo>();
            if (!DevTools.QuietMode)
                Log("Unloading DevTool's module: " + moduleName + " (" + moduleType.FullName + ")");
            
            // Call all OnGameHasUnloaded methods from the type and remove registered methods.
            foreach (var method in _onGameHasUnloadedMethods)
            {
                if (method.DeclaringType != moduleType) continue;
                
                method.Invoke(null, null);
                methodsToRemove.Add(method);
            }
            
            foreach (var method in methodsToRemove)
                _onGameHasUnloadedMethods.Remove(method);
            
            // Remove all OnGameHasLoaded methods registered.
            methodsToRemove = new List<MethodInfo>();
            
            foreach (var method in _onGameHasLoadedMethods)
            {
                if (method.DeclaringType != moduleType) continue;
                
                methodsToRemove.Add(method);
            }
            
            foreach (var method in methodsToRemove)
                _onGameHasLoadedMethods.Remove(method);
            
            // Unregister related KeyboardShortcuts
            var keyboardShortcutMethods = GetMethodsWithAttribute(typeof(InGameKeyboardShortcutAttribute), moduleType);

            foreach (var method in keyboardShortcutMethods)
            {
                var attr = (InGameKeyboardShortcutAttribute) Attribute.GetCustomAttribute(method,
                    typeof(InGameKeyboardShortcutAttribute));
                ActionManager.UnregisterAction(attr.Key, attr.ActionName);
            }
        }

        private static void Log(string message) => Loggr.Log(message, ConsoleColor.DarkYellow);

        private static void RegisterModule(Type moduleType)
        {
            var moduleName = GetDevToolsModuleName(moduleType);

            if (moduleName == "--invalid--")
                return;
            
            // Register methods declared with OnGameHasLoaded attribute
            RegisterOnGameHasLoadedMethods(moduleType);
            
            // Register methods declared with OnGameHasUnloaded attribute
            RegisterOnGameHasUnloadedMethods(moduleType);

            // Map keyboard shortcuts declared with InGameKeyboardShortcut attribute
            var keyboardShortcutMethods = GetMethodsWithAttribute(typeof(InGameKeyboardShortcutAttribute), moduleType);
            
            if (!DevTools.QuietMode)
                Loggr.Log("[" + moduleName + "]", ConsoleColor.Gray);
            
            foreach (var method in keyboardShortcutMethods)
            {
                var attr = (InGameKeyboardShortcutAttribute) Attribute.GetCustomAttribute(method,
                    typeof(InGameKeyboardShortcutAttribute));
                ActionManager.RegisterAction(attr.Key, attr.ActionName, method);
            }
        }

        private static IEnumerable<MethodInfo> GetMethodsWithAttribute(Type attributeType, Type moduleType)
        {
            var methods =
                from method in moduleType.GetMethods()
                where method.IsDefined(attributeType, true)
                select method;

            return methods;
        }

        private static void RegisterOnGameHasLoadedMethods(Type moduleType)
        {
            var onGameHasLoadedMethods = GetMethodsWithAttribute(typeof(OnGameHasLoadedAttribute), moduleType);

            foreach (var method in onGameHasLoadedMethods) _onGameHasLoadedMethods.Add(method);
        }
        
        private static void RegisterOnGameHasUnloadedMethods(Type moduleType)
        {
            var onGameHasUnloadedMethods = GetMethodsWithAttribute(typeof(OnGameHasUnloadedAttribute), moduleType);

            foreach (var method in onGameHasUnloadedMethods) _onGameHasUnloadedMethods.Add(method);
        }

        private static string GetDevToolsModuleName(Type moduleType)
        {
            var moduleName = moduleType.Name;
            var namePropInfo = moduleType.GetProperty("Name", BindingFlags.Static | BindingFlags.Public);
            if (namePropInfo != null)
            {
                moduleName = (string) namePropInfo.GetValue(null);
            }

            return moduleName;
        }

        private static void ExecuteOnGameHasLoadedMethods()
        {
            foreach (var onGameHasLoadedMethod in _onGameHasLoadedMethods) onGameHasLoadedMethod.Invoke(null, null);
        }
        
        private static void ExecuteOnGameHasUnloadedMethods()
        {
            foreach (var onGameHasUnloadedMethod in _onGameHasUnloadedMethods) onGameHasUnloadedMethod.Invoke(null, null);
        }
    }
}