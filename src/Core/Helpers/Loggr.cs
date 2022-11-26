using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Linq;
using BepInEx;
using Modding.Humankind.DevTools.DeveloperTools;

namespace Modding.Humankind.DevTools
{
    /// <summary>
    ///     A static logger with color support, only for printing to BepInEx's console window.
    /// </summary>
    public class Loggr
    {
        private static bool _devlogEnabled = true;
        private static bool _debuglogEnabled = true;
        private static bool _announceEnabled = true;
        private static bool _initialized;
        private static bool _isConsoleLoggerEnabled = false;
        private static bool _isDiskLoggerEnabled = false;

        private static PropertyInfo _consoleStream;
        private static MethodInfo _setConsoleColor;
        private static Type _consoleManager;

        public static bool WriteLogToDisk { get; set; } = false;

        /// <summary>
        ///     Sets or gets whether the output to console of internal calls to <c>Loggr.LogInfo</c>, <c>Loggr.LogWarning</c>,
        ///     <c>Loggr.LogError</c> and also public calls to <c>Loggr.Log</c> are enabled or not. 
        /// </summary>
        public static bool IsDevelopmentLogEnabled
        {
            get => _devlogEnabled;
            set {
                if (value != _devlogEnabled)
                    _Log("Development log " + (value ? "enabled." : "disabled."), ConsoleColor.White);
                
                _devlogEnabled = value;
            }
        }

        internal static void LogInfo(string message)
        {
            try
            {
                if (_devlogEnabled) DevTools.Log.LogInfo(message);
            }
            catch (Exception)
            {
                // ignore
            }
        }

        internal static void LogWarning(string message)
        {
            try
            {
                if (_devlogEnabled) 
                    DevTools.Log.LogWarning(message);
                
                if (WriteLogToDisk)
                    WriteToDisk(message);
            }
            catch (Exception)
            {
                // ignore
            }
        }

        internal static void LogError(string message)
        {
            try
            {
                if (_devlogEnabled) 
                    DevTools.Log.LogError(message);
                
                if (WriteLogToDisk)
                    WriteToDisk(message);
            }
            catch (Exception)
            {
                // ignore
            }
        }

        public static void Debug(string message)
        {
            if (_debuglogEnabled) _Log(message, ConsoleColor.DarkCyan);
        }

        public static void Announce(string message)
        {
            Announce(message, ConsoleColor.White);
        }
        
        public static void Announce(string message, ConsoleColor color)
        {
            if (_announceEnabled)
                _Log(message, color);
        }

        /// <summary>
        ///     Prints a <c>message</c> to BepInEx's console with <c>defaultColor</c> as default text color. Use `%Color%` inline to change text color, where `Color` is any value within <c>ConsoleColor</c> enum.
        /// Available colors are: Default, Black, DarkBlue, DarkGreen, DarkCyan, DarkRed, DarkMagenta, DarkYellow, Gray, DarkGray, Blue, Green, Cyan, Red, Magenta, Yellow, White.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="defaultColor"></param>
        /// <param name="appendNewLine"></param>
        public static void Log(string message, ConsoleColor defaultColor, bool appendNewLine = true)
        {
            if (!_devlogEnabled)
                return;

            _LogEx(message, defaultColor, appendNewLine);
        }

        public static void Log(Exception ex, 
            [CallerLineNumber] int lineNumber = 0,
            [CallerMemberName] string caller = null)
        {
            _LogEx(ex.ToString(), ConsoleColor.Red, true);
            _LogEx("[IN " + caller + " @ " + lineNumber + "]", ConsoleColor.Red, true);
        }
        
        public static void Log(string message) => Log(message, ConsoleColor.White);
        
        public static void Log(object obj) => Log(obj, ConsoleColor.White);
        
        public static void Log(object obj, ConsoleColor defaultColor)
        {
            if (!_devlogEnabled)
                return;
            
            _LogEx((new PrintableObject(obj)).ToString(), defaultColor, true);
        }
        
        public static void LogAll(object obj) => LogAll(obj, ConsoleColor.White);
        
        public static void LogAll(object obj, ConsoleColor defaultColor)
        {
            if (!_devlogEnabled)
                return;
            
            _LogEx((new PrintableObject(obj)
            {
                NonPublicFields = true,
                NonPublicProperties = true,
                Methods = true
            }).ToString(), defaultColor, true);
        }
        
        private static void _Log(string message, ConsoleColor color)
        {
            if (!_initialized)
                Initialize();
            
            if (!_isConsoleLoggerEnabled)
                return;
            
            _setConsoleColor.Invoke(null, new object[] {color});
            ((TextWriter) _consoleStream.GetValue(_consoleManager, null)).Write(message + Environment.NewLine);
            _setConsoleColor.Invoke(null, new object[] {ConsoleColor.Gray});
        }

        private static void Initialize()
        {
            var bepInExAss = Assembly.GetAssembly(typeof(BaseUnityPlugin));

            try
            {
                var type = bepInExAss.GetType("BepInEx.ConsoleManager");

                _consoleStream = type.GetProperty("ConsoleStream", BindingFlags.Static | BindingFlags.Public);
                _setConsoleColor = type.GetMethod("SetConsoleColor", BindingFlags.Static | BindingFlags.Public);
                _consoleManager = type;

                _setConsoleColor.Invoke(null, new object[] {ConsoleColor.DarkGray});
                ((TextWriter) _consoleStream.GetValue(_consoleManager, null)).Write("DevTools.Loggr initialized." + Environment.NewLine);
                _setConsoleColor.Invoke(null, new object[] {ConsoleColor.Gray});

                _isConsoleLoggerEnabled = true;
            }
            catch (Exception)
            {
                _isConsoleLoggerEnabled = false;
            }

            try
            {
                if (BepInEx.Logging.Logger.Listeners.FirstOrDefault(l =>
                        l is BepInEx.Logging.DiskLogListener) is
                    BepInEx.Logging.DiskLogListener diskLogger)
                {
                    if (_isConsoleLoggerEnabled)
                    {
                        diskLogger.LogWriter.Write("DevTools.Loggr initialized." + Environment.NewLine);
                    }
                    else
                    {
                        diskLogger.LogWriter.Write("DevTools.Loggr initialized in disk mode only." + Environment.NewLine);
                    }

                    _isDiskLoggerEnabled = true;
                }
            }
            catch (Exception)
            {
                _isDiskLoggerEnabled = false;
            }
            
            _initialized = true;
        }

        private static void _LogEx(string message, ConsoleColor defaultColor, bool appendNewLine = true)
        {
            if (!_initialized)
                Initialize();

            TextWriter writer = (TextWriter) (_isConsoleLoggerEnabled ? _consoleStream.GetValue(_consoleManager, null) : null);
            ConsoleColor color;
            bool lastWasMatch = true;
            bool match = false;
            int ignore;
            
            _setConsoleColor.Invoke(null, new object[] {defaultColor});

            string[] parts = message.Split('%');
            
            foreach (var text in parts)
            {
                match = false;

                if (text.Length < 12 && !int.TryParse(text, out ignore))
                {
                    if (ConsoleColor.TryParse(text, true, out color))
                    {
                        match = true;
                        if (_isConsoleLoggerEnabled)
                            _setConsoleColor.Invoke(null, new object[] {color});
                    }
                    else if (text.ToUpper() == "DEFAULT")
                    {
                        match = true;
                        if (_isConsoleLoggerEnabled)
                            _setConsoleColor.Invoke(null, new object[] {defaultColor});
                    }
                }
                
                if (!match)
                {
                    if (!lastWasMatch)
                    {
                        if (_isConsoleLoggerEnabled)
                            writer.Write("%" + text);
                        if (WriteLogToDisk)
                            WriteToDisk("%" + text);
                    }
                    else
                    {
                        if (_isConsoleLoggerEnabled)
                            writer.Write(text);
                        if (WriteLogToDisk)
                            WriteToDisk(text);
                    }
                }

                lastWasMatch = match;
            }
            
            if (appendNewLine)
            {
                if (_isConsoleLoggerEnabled)
                    writer.Write(Environment.NewLine);
                if (WriteLogToDisk)
                    WriteToDisk(Environment.NewLine);
            }
            if (_isConsoleLoggerEnabled)
                _setConsoleColor.Invoke(null, new object[] {ConsoleColor.Gray});
        }

        private static void WriteToDisk(string message)
        {
            if (!_isDiskLoggerEnabled) return;
            
            if (BepInEx.Logging.Logger.Listeners.FirstOrDefault(l =>
                    l is BepInEx.Logging.DiskLogListener) is
                BepInEx.Logging.DiskLogListener diskLogger)
            {
                diskLogger.LogWriter.Write(message);
            }
        }
    }
}
