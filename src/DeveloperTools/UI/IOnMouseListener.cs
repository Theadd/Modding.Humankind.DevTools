
using Amplitude.UI.Interactables;
using UnityEngine;

namespace Modding.Humankind.DevTools.DeveloperTools.UI
{
    public interface IOnMouseListener
    {
        void OnMouseEnter(IUIControl control, Vector2 coords);
        void OnMouseLeave(IUIControl control, Vector2 coords);
    }
}
