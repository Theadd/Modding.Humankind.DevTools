using System;

namespace Modding.Humankind.DevTools
{
    /// <summary>
    ///     In order to use any other DevTool's attribute in a class member, that class must be annotated with this
    ///     attribute for them to work as they're only searched in classes with the <c>DevToolsModule</c> annotation.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class DevToolsModuleAttribute : Attribute
    {
    }
}