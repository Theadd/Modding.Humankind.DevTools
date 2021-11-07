using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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
        }

        public static void FindAllAndRegister()
        {
            Initialize();

            if (DevTools.IncludeExternalModules)
            {
                var allAIStrategyModules =
                    from assembly in AppDomain.CurrentDomain.GetAssemblies().AsParallel()
                    from type in assembly.GetTypes()
                    where type.IsDefined(typeof(DevToolsModuleAttribute), false)
                    select type;

                allAIStrategyModules.ForAll(RegisterModule);
            }
            else
            {
                var ownAIStrategyModules =
                    from type in Assembly.GetExecutingAssembly().GetTypes()
                    where type.IsDefined(typeof(DevToolsModuleAttribute), false)
                    select type;

                foreach (var aiStrategyModule in ownAIStrategyModules) RegisterModule(aiStrategyModule);
            }
        }

        private static void RegisterModule(Type aiStrategyModule)
        {
            // Register methods declared with OnGameHasLoaded attribute
            var onGameHasLoadedMethods =
                from method in aiStrategyModule.GetMethods()
                where method.IsDefined(typeof(OnGameHasLoadedAttribute), true)
                select method;

            foreach (var method in onGameHasLoadedMethods) _onGameHasLoadedMethods.Add(method);
            
            // Register methods declared with OnGameHasUnloaded attribute
            var onGameHasUnloadedMethods =
                from method in aiStrategyModule.GetMethods()
                where method.IsDefined(typeof(OnGameHasUnloadedAttribute), true)
                select method;

            foreach (var method in onGameHasUnloadedMethods) _onGameHasUnloadedMethods.Add(method);

            // Map keyboard shortcuts declared with InGameKeyboardShortcut attribute
            var keyboardShortcutMethods =
                from method in aiStrategyModule.GetMethods()
                where method.IsDefined(typeof(InGameLeftControlShortcutAttribute), true)
                select method;

            foreach (var method in keyboardShortcutMethods)
            {
                var attr = (InGameLeftControlShortcutAttribute) Attribute.GetCustomAttribute(method,
                    typeof(InGameLeftControlShortcutAttribute));
                ActionManager.RegisterAction(attr.Key, attr.ActionName, method);
            }
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