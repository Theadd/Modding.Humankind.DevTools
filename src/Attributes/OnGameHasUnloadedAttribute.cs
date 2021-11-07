using System;

namespace Modding.Humankind.DevTools
{
    /// <summary>
    ///     Any static method with the [OnGameHasUnloaded] annotation will be called everytime a running game ends.
    /// </summary>
    /// <remarks>The method's class must be annotated with [DevToolsModule] for this to work.</remarks>
    [AttributeUsage(AttributeTargets.Method)]
    public class OnGameHasUnloadedAttribute : Attribute
    {
    }
}