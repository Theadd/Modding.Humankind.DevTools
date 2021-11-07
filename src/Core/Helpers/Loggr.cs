using System;
using System.IO;
using System.Reflection;
using BepInEx;

namespace Modding.Humankind.DevTools.Core
{
    public class Loggr
    {
        private static bool _devlogEnabled = true;
        private static bool _debuglogEnabled = true;
        private static bool _announceEnabled = true;
        private static bool _initialized;

        private static PropertyInfo _consoleStream;
        private static MethodInfo _setConsoleColor;
        private static Type _consoleManager;

        /// <summary>
        ///     Sets or gets whether the output to console of calls to <c>LogInfo</c>, <c>LogWarning</c> and
        ///     <c>LogError</c> is enabled or not. 
        /// </summary>
        public static bool IsDevelopmentLogEnabled
        {
            get => _devlogEnabled;
            set {
                if (value != _devlogEnabled)
                    Announce("Development log " + (value ? "enabled." : "disabled."));
                
                _devlogEnabled = value;
            }
        }

        internal static void LogInfo(string message)
        {
            if (_devlogEnabled) DevTools.Log.LogInfo(message);
        }

        internal static void LogWarning(string message)
        {
            if (_devlogEnabled) DevTools.Log.LogWarning(message);
        }

        internal static void LogError(string message)
        {
            if (_devlogEnabled) DevTools.Log.LogError(message);
        }

        public static void Debug(string message)
        {
            if (_debuglogEnabled) Log(message, ConsoleColor.DarkCyan);
        }

        public static void Announce(string message)
        {
            if (_announceEnabled)
                _Log(message, ConsoleColor.White);
        }

        public static void Log(string message, ConsoleColor color)
        {
            if (!_devlogEnabled)
                return;

            if (!_initialized)
                Initialize();

            _setConsoleColor.Invoke(null, new object[] {color});
            ((TextWriter) _consoleStream.GetValue(_consoleManager, null)).Write(message + Environment.NewLine);
            _setConsoleColor.Invoke(null, new object[] {ConsoleColor.Gray});
        }
        
        private static void _Log(string message, ConsoleColor color)
        {
            if (!_initialized)
                Initialize();

            _setConsoleColor.Invoke(null, new object[] {color});
            ((TextWriter) _consoleStream.GetValue(_consoleManager, null)).Write(message + Environment.NewLine);
            _setConsoleColor.Invoke(null, new object[] {ConsoleColor.Gray});
        }

        private static void Initialize()
        {
            var bepInExAss = Assembly.GetAssembly(typeof(BaseUnityPlugin));
            var type = bepInExAss.GetType("BepInEx.ConsoleManager");

            _consoleStream = type.GetProperty("ConsoleStream", BindingFlags.Static | BindingFlags.Public);
            _setConsoleColor = type.GetMethod("SetConsoleColor", BindingFlags.Static | BindingFlags.Public);
            _consoleManager = type;

            _initialized = true;
        }
    }
}