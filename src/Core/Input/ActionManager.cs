using System;
using System.Collections.Generic;
using System.Linq;
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
        internal static Dictionary<KeyboardShortcut, string> _mappedActions;
        internal static Dictionary<string, MethodInfo> _mappedMethods;
        private static string _currentActionType = "";
        private static KeyboardShortcut _currentKeyboardShortcut = KeyboardShortcut.Empty;
        private static bool _hasActiveAction;
        // private static bool _isListening = true;
        // private static bool _iLSA;  // Short name for _isListeningShortcutActive

        private static int _counter;

        private static int _counterCap = 200;

        public static void Unload()
        {
            _enabled = false;
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

            /*if (!SandboxManager.IsStarted)
                return;
            
            if (_service.GetKey(KeyCode.LeftControl) && _service.GetKey(KeyCode.K))
            {
                if (_iLSA) return;

                _isListening = !_isListening;
                _iLSA = true;
                Loggr.Announce("Listening to keyboard shortcut actions is: " + (_isListening ? "Enabled" : "Disabled"));
                return;
            }
            else
            {
                if (_iLSA)
                    _iLSA = false;
            }*/
            if (/*_isListening*/ true)
            {
                if (_hasActiveAction)
                {
                    if (_currentKeyboardShortcut.IsDown())
                        return;

                    _hasActiveAction = false;
                    _currentKeyboardShortcut = KeyboardShortcut.Empty;
                    _currentActionType = "";
                }

                if (RuntimeActionMapper.GetFirstShortcutMatching(out string actionName, out KeyboardShortcut key))
                {
                    _hasActiveAction = true;
                    _currentKeyboardShortcut = key;
                    _currentActionType = actionName;
                    
                    RuntimeActionMapper.InvokeAction(_currentActionType);
                }
            }
        }

        public static bool RegisterAction(KeyboardShortcut key, string actionName, MethodInfo staticMethodInfo)
        {
            if (actionName == "")
                return false;

            if (_mappedActions.ContainsKey(key))
            {
                if (_mappedActions[key] != actionName)
                {
                    Loggr.LogError("Unable to register key [" + key +
                                   "] since it's already registered to a different action name.");
                    return false;
                }

                _mappedMethods[actionName] = staticMethodInfo;
            }
            else
            {
                if (_mappedMethods.ContainsKey(actionName))
                {
                    var prevKey = _mappedActions.FirstOrDefault(x => x.Value == actionName).Key;
                    _mappedActions.Remove(prevKey);
                    _mappedActions.Add(key, actionName);
                    _mappedMethods[actionName] = staticMethodInfo;

                }
                else
                {
                    _mappedActions.Add(key, actionName);
                    _mappedMethods.Add(actionName, staticMethodInfo);
                }
            }

            if (!DevTools.QuietMode)
                Loggr.Log("\t%Green%[" + key + "]%Gray% => %Default%" + actionName, ConsoleColor.White);

            return true;
        }
        
        public static void UnregisterAction(KeyboardShortcut key, string actionName)
        {
            if (_mappedActions.TryGetValue(key, out string expectedActionName))
            {
                if (expectedActionName != actionName)
                {
                    Loggr.LogError("Unable to unregister key [" + key + "] due to registered actionName '" + 
                                   expectedActionName + "' differs from provided action name to unregister '" + 
                                   actionName + "'.");
                    return;
                }
            }
            else
            {
                Loggr.LogError("Unable to unregister key [" + key + "], key not found.");
                return;
            }

            _mappedActions.Remove(key);
            _mappedMethods.Remove(actionName);
        }

        private static void Initialize()
        {
            Reset();
        }

        internal static void Reset()
        {
            _mappedActions = new Dictionary<KeyboardShortcut, string>();
            _mappedMethods = new Dictionary<string, MethodInfo>();
        }

        
    }
}
