using System;
using Amplitude.Framework.Overlay;
using Amplitude.Mercury.Overlay;
using UnityEngine;
using Modding.Humankind.DevTools;

namespace Modding.Humankind.DevTools.DeveloperTools.UI
{
    public static class WindowUtils
    {
        public static void DrawWindowTitleBar<T>(T instance) where T : PopupWindow
        {
            string title = instance.Title;
            if (instance is FloatingToolWindow uiToolWindow)
                title = uiToolWindow.WindowTitle;
            
            GUILayout.BeginHorizontal("PopupWindow.Title.Banner");
            GUILayout.Label(title, "PopupWindow.Title");
            GUILayout.FlexibleSpace();

                if (GUILayout.Button("_", "PopupWindow.Title.Button"))
                {
                    UIController.HideWindow<T>();
                }
                if (GUILayout.Button("X", "PopupWindow.Title.Button"))
                {
                    UIController.CloseWindow<T>();
                }
            
            GUILayout.EndHorizontal();
        }

        /*public static void TEMP()
        {
            Amplitude.Framework.Profiling.ProfilerWindow a;
            Amplitude.Graphics.Profiling.GPUProfilerWindow.SampleHistoryDrawCommand x;
        }*/
    }
}
