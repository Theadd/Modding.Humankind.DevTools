using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Amplitude.Framework;
using Amplitude.Framework.Input;
using Amplitude.Mercury.Sandbox;
using UnityEngine;
using BepInEx.Configuration;

namespace Modding.Humankind.DevTools.Core
{
    internal static class ActionManager
    {
        private static bool _enabled;
        private static int _runsToEnable = 20;
        private static bool _active;
        private static IInputService _service;
        private static Dictionary<KeyCode, string> _mappedActions;
        private static Dictionary<string, MethodInfo> _mappedMethods;
        private static string _currentActionType = "";
        private static KeyCode _currentKeyCode = KeyCode.None;
        private static bool _hasActiveAction;

        private static int _counter;

        private static int _counterCap = 200;

        public static void Unload()
        {
            _enabled = false;
            // TODO: When reloading this plugin from scripts at runtime (using Script Engine), it should
            //       enable immediately without having to wait for it.
            _runsToEnable = 20;
            _active = false;
            _counter = 0;
        }

        private static void ResetSyncSpeedValues()
        {
            var rate = GameController.Instance.SyncRate;

            _counterCap = (int) (200 * rate);
        }

        public static void Run()
        {
            if (!_active)
            {
                if (!_enabled)
                {
                    _runsToEnable -= 1;
                    if (_runsToEnable <= 0)
                    {
                        _enabled = true;
                        ResetSyncSpeedValues();
                    }

                    return;
                }

                var service = Services.GetService<IInputService>();
                if (service != default(IInputService))
                {
                    _service = service;
                    Initialize();
                    _active = true;
                    _runsToEnable = 50;
                }

                return;
            }

            _counter += 1;

            if (_counter >= _counterCap)
            {
                GameController.SynchronizeGameState();
                _counter = 0;
                ResetSyncSpeedValues();
            }

            if (!SandboxManager.IsStarted)
                return;

            if (_service.GetKey(KeyCode.LeftControl))
            {
                if (_hasActiveAction)
                {
                    if (_service.GetKeyDown(_currentKeyCode))
                        return;

                    _hasActiveAction = false;
                    _currentKeyCode = KeyCode.None;
                    _currentActionType = "";
                }

                foreach (var action in _mappedActions)
                    if (_service.GetKeyDown(action.Key))
                    {
                        _hasActiveAction = true;
                        _currentKeyCode = action.Key;
                        _currentActionType = action.Value;

                        Execute(_currentActionType);
                    }
            }
        }

        public static void RegisterAction(KeyCode key, string actionName, MethodInfo staticMethodInfo)
        {
            if (actionName == "")
                return;

            if (_mappedActions.ContainsKey(key))
                return;

            _mappedActions.Add(key, actionName);
            _mappedMethods.Add(actionName, staticMethodInfo);

            Loggr.Debug("Successfully registered action " + actionName + " to [LeftCtrl] + [" + key + "]");
        }

        private static void Initialize()
        {
            Loggr.Debug("Initializing ActionManager...");
            _mappedActions = new Dictionary<KeyCode, string>();
            _mappedMethods = new Dictionary<string, MethodInfo>();
        }

        public static void Execute(string actionType)
        {
            if (_mappedMethods.TryGetValue(actionType, out var method))
            {
                method.Invoke(null, null);
                Loggr.Announce("Action " + actionType + " executed.");
            }
            else
            {
                Loggr.LogError("Action " + actionType + " NOT Found.");
            }
        }
    }
}