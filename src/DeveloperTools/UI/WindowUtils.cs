using System;
using Amplitude.Framework.Overlay;
using UnityEngine;

namespace Modding.Humankind.DevTools.DeveloperTools.UI
{
    public class WindowUtils
    {
        public static void DrawWindowHeader<T>(T instance) where T : FloatingWindow
        {
            GUILayout.BeginHorizontal("PopupWindow.Title.Banner", Array.Empty<GUILayoutOption>());
            GUILayout.Label(instance.Title, "PopupWindow.Title", Array.Empty<GUILayoutOption>());
            GUILayout.FlexibleSpace();

                if (GUILayout.Button("_", "PopupWindow.Title.Button", Array.Empty<GUILayoutOption>()))
                {
                    UIManager.HideWindow<T>();
                }
                if (GUILayout.Button("X", "PopupWindow.Title.Button", Array.Empty<GUILayoutOption>()))
                {
                    UIManager.CloseWindow<T>();
                }
            
            GUILayout.EndHorizontal();
        }
    }
}