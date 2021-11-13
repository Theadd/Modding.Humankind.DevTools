using System;

namespace Modding.Humankind.DevTools
{
    /// <summary>
    ///     Any static method with the <c>OnGameHasLoaded</c> annotation will be called everytime a game has loaded and it
    ///     is ready to play.
    /// </summary>
    /// <remarks>The method's class must be annotated with <c>DevToolsModule</c> for this to work.</remarks>
    [AttributeUsage(AttributeTargets.Method)]
    public class OnGameHasLoadedAttribute : Attribute
    {
    }
}