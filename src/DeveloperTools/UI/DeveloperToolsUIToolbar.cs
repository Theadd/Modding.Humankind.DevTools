using System;
using Amplitude.Framework.Overlay;
using Amplitude.Mercury.Overlay;
using UnityEngine;

namespace Modding.Humankind.DevTools.DeveloperTools.UI
{

    public class DeveloperToolsUIToolbar : FloatingWindow, IPopupWindowWithAdjustableWindowHeight, IGetWindowRect
    {
        protected Rect windowRect = new Rect (30, 290, 170, 600);
        
        protected override void Awake()
        {
            this.WindowStartupLocation = new Vector2(300f, 200f);
            this.Title = "Toolbar";
            base.Awake();
            this.Width = 200f;
        }
        
        protected void OnGUI () {
            if (IsVisible)
            {
                GUI.skin = UIManager.DefaultSkin;
                GUI.color = Color.white;
                GUI.backgroundColor = Color.white;
                GUI.enabled = true;
                windowRect = GUI.Window (0, windowRect, OnDrawToolbarWindow, string.Empty, "PopupWindow.Sidebar");
            }
        }

        protected void OnDrawToolbarWindow(int windowID) {
            GUILayout.BeginArea(new Rect(0f, 0f, windowRect.width, windowRect.height));
            GUILayout.BeginVertical((GUIStyle) "Widget.ClientArea");
        
            GUILayout.Label("<color=#000000AA>C H E A T I N G</color>   T O O L S", "PopupWindow.Sidebar.Heading");
            OnDrawTool<MilitaryCheatsWindow>("Military");
            OnDrawTool<TechnologyUtilsWindow>("Technologies");
            OnDrawTool<TechnologiesWindow>("Technologies v2.0");
            OnDrawTool<ResourcesUtilsWindow>("Resources");
            OnDrawTool<AffinityUtilsWindow>("Cultural Affinity");
            GUILayout.Label("<color=#000000AA>P R O F I L I N G</color>   T O O L S", "PopupWindow.Sidebar.Heading");
            OnDrawTool<FramerateWindow>("Framerate");
            GUILayout.Label("<color=#000000AA>D E V E L O P E R</color>   T O O L S", "PopupWindow.Sidebar.Heading");
            OnDrawTool<AutoTurnWindow>("Auto Turn <color=#00CC00AA>**HOT**</color>");
            OnDrawTool<ArchetypesWindow>("Archetypes");
            GUILayout.Label("E X P <color=#000000AA>E R I M E N T A L</color>", "PopupWindow.Sidebar.Heading");
            OnDrawTool<AICursorWindow>("AI Cursor");
            OnDrawTool<AIWindow>("AI");
                
            GUILayout.EndVertical();
            
            if (Event.current.type == EventType.Repaint)
            {
                Rect lastRect = GUILayoutUtility.GetLastRect();
                if ((double) lastRect.height > (double) this.Height || (double) lastRect.height < (double) this.Height)
                {
                    this.Height = lastRect.height;
                    windowRect.height = lastRect.height;
                }
            }
            
            GUILayout.EndArea();

            GUI.DragWindow (new Rect (0,0,10000,10000));
        }

        protected void OnDrawTool<T>(string tool) where T : FloatingWindow
        {
            var window = UIManager.GetWindow<T>(false);
            var created = window != null;
            var visible = created && window.IsVisible;

            GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
                // The close tool button (Object.Destroy)
                GUI.enabled = created;
                GUI.color = created ? Color.white : Color.clear;
                if (GUILayout.Button("x", "PopupWindow.Sidebar.InlineButton", new GUILayoutOption[] {
                    GUILayout.Width(28)
                })) {
                    UIManager.CloseWindow<T>();
                }
                GUI.enabled = true;
                GUI.color = Color.white;

                // The show/hide tool button toggle
                var shouldBeVisible = (GUILayout.Toggle(visible, tool.ToUpper(), "PopupWindow.Sidebar.Toggle"));
                if (visible != shouldBeVisible)
                {
                    UIManager.ShowWindow<T>(shouldBeVisible);
                }
            GUILayout.EndHorizontal();
        }
        
        protected override void OnDrawWindowClientArea(int instanceId) { }
        
        protected override void OnBecomeVisible() => this.SyncUIOverlay(base.OnBecomeVisible);
        protected override void OnBecomeInvisible() => this.SyncUIOverlay(base.OnBecomeInvisible);
        public Rect GetWindowRect() => windowRect;
        public bool ShouldBeVisible => HumankindGame.IsGameLoaded;
    }
}
