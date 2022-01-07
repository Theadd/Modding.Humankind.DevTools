
using UnityEngine;

namespace Modding.Humankind.DevTools.DeveloperTools.UI
{
    public interface IToolWindow
    {
        Rect GetWindowRect();
        void SetWindowRect(Rect rect);
        bool ShouldBeVisible { get; }
        bool ShouldRestoreLastWindowPosition { get; }
        void OnWritePlayerPreferences();
        void OnReadPlayerPreferences();
        void Close(bool saveVisibilityStateBeforeClosing = false);
    }
}
