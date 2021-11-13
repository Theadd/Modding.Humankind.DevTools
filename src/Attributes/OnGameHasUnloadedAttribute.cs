using System;

namespace Modding.Humankind.DevTools
{
    /// <summary>
    ///     Any static method with the <c>OnGameHasUnloaded</c> annotation will be called everytime a running game ends.
    /// </summary>
    /// <remarks>The method's class must be annotated with <c>DevToolsModule</c> for this to work.</remarks>
    [AttributeUsage(AttributeTargets.Method)]
    public class OnGameHasUnloadedAttribute : Attribute
    {
    }
}