using System;
using Amplitude.Framework.Overlay;
using UnityEngine;
using Modding.Humankind.DevTools;

namespace Modding.Humankind.DevTools.DeveloperTools.UI
{
    public static class WindowUtils
    {
        public static void DrawWindowTitleBar<T>(T instance) where T : PopupWindow
        {
            string title = instance.Title;
            if (instance is UIToolWindow uiToolWindow)
                title = uiToolWindow.WindowTitle;
            
            GUILayout.BeginHorizontal("PopupWindow.Title.Banner");
            GUILayout.Label(title, "PopupWindow.Title");
            GUILayout.FlexibleSpace();

                if (GUILayout.Button("_", "PopupWindow.Title.Button"))
                {
                    UIManager.HideWindow<T>();
                }
                if (GUILayout.Button("X", "PopupWindow.Title.Button"))
                {
                    UIManager.CloseWindow<T>();
                }
            
            GUILayout.EndHorizontal();
        }
    }
}
