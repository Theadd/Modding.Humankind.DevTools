using System;
using Amplitude.Framework.Overlay;
using Amplitude.Mercury.Overlay;
using UnityEngine;

namespace Modding.Humankind.DevTools.DeveloperTools.UI
{

    public class DeveloperToolsUIToolbar : FloatingWindow, IPopupWindowWithAdjustableWindowHeight
    {
        public static GUISkin Skin = null;
        Rect windowRect = new Rect (30, 290, 170, 200);
        
        protected override void Awake()
        {
            this.WindowStartupLocation = new Vector2(300f, 200f);
            this.Title = "Toolbar";
            base.Awake();
            this.Width = 200f;
        }
        
        void OnGUI () {
            if (IsVisible)
            {
                GUI.skin = UIManager.DefaultSkin;
                GUI.color = Color.white;
                GUI.backgroundColor = Color.white;
                GUI.enabled = true;
                windowRect = GUI.Window (0, windowRect, OnDrawToolbarWindow, string.Empty, "PopupWindow");
            }
        }

        void OnDrawToolbarWindow(int windowID) {
            GUILayout.BeginArea(new Rect(0f, 0f, windowRect.width, windowRect.height));
            GUILayout.BeginVertical((GUIStyle) "Widget.ClientArea");
        
            GUILayout.Label("C H E A T I N G   T O O L S", "PopupWindow.Heading1");
            OnDrawTool<TechnologyUtilsWindow>("Technology Utils");
            OnDrawTool<ResourcesUtilsWindow>("Resources Utils");
            OnDrawTool<MilitaryCheatsWindow>("Military Cheats");
            GUILayout.Label("P R O F I L I N G   T O O L S", "PopupWindow.Heading1");
            OnDrawTool<FramerateWindow>("Framerate");
                
            GUILayout.EndVertical();
            GUILayout.EndArea();

            GUI.DragWindow (new Rect (0,0,10000,10000));
        }

        void OnDrawTool<T>(string toolName) where T : FloatingWindow
        {
            OnDrawTool<T>(new GUIContent(toolName, toolName));
        }

        void OnDrawTool<T>(GUIContent tool) where T : FloatingWindow
        {
            var window = UIManager.GetWindow<T>(false);
            var created = window != null;
            var visible = created && window.IsVisible;

            GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
                // The close tool button (Object.Destroy)
                GUI.enabled = created;
                if (GUILayout.Button("X", "PopupWindow.Title.Button", Array.Empty<GUILayoutOption>()))
                {
                    UIManager.CloseWindow<T>();
                }
                GUI.enabled = true;

                // The show/hide tool button toggle
                var shouldBeVisible = (GUILayout.Toggle(visible, tool, Array.Empty<GUILayoutOption>()));
                if (visible != shouldBeVisible)
                {
                    UIManager.ShowWindow<T>(shouldBeVisible);
                }
            GUILayout.EndHorizontal();
        }
        
        protected override void OnDrawWindowClientArea(int instanceId)
        {
            
        }
    }
}
