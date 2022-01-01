
using UnityEngine;

namespace Modding.Humankind.DevTools.DeveloperTools.UI
{
    public interface IGetWindowRect
    {
        Rect GetWindowRect();
        bool ShouldBeVisible { get; }
    }
}
