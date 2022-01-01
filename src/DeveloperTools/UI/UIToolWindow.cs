using System;
using Amplitude.Framework.Overlay;
using Amplitude.Mercury.Overlay;
using Amplitude.UI;
using UnityEngine;

namespace Modding.Humankind.DevTools.DeveloperTools.UI
{

    public abstract class UIToolWindow : FloatingWindow, IGetWindowRect
    {
        public abstract string WindowTitle { get; set; }
        public virtual Rect WindowRect { get; set; } = new Rect (300, 300, 300, 300);
        public virtual string WindowGUIStyle { get; set; } = "PopupWindow.Sidebar";
        public virtual bool ShouldBeVisible { get; set; } = true;
        public int WindowID => Math.Abs(GetInstanceID());

        public virtual void OnGUIStyling()
        {
            GUI.skin = UIManager.DefaultSkin;
            GUI.color = Color.white;
            GUI.backgroundColor = Color.white;
            GUI.enabled = true;
        }

        public abstract void OnDrawUI();
        
        // Private members //
        #region PRIVATE
        protected override void Awake()
        {
            WindowStartupLocation = new Vector2(WindowRect.x, WindowRect.y);
            Title = string.IsNullOrEmpty(Title) ? "UI TOOL WINDOW" : Title;
            base.Awake();
            Width = WindowRect.width;
        }
        
        void OnGUI () {
            if (IsVisible && ShouldBeVisible)
            {
                OnGUIStyling();
                WindowRect = GUI.Window (WindowID, WindowRect, OnDrawUIToolWindow, string.Empty, WindowGUIStyle);
            }
        }

        void OnDrawUIToolWindow(int windowID) {
            GUILayout.BeginArea(new Rect(0f, 0f, WindowRect.width, WindowRect.height));

            OnDrawUI();
            
            if (Event.current.type == EventType.Repaint)
            {
                Rect lastRect = GUILayoutUtility.GetLastRect();
                if (((int)lastRect.height + (int)lastRect.y) != (int)WindowRect.height)
                {
                    Height = lastRect.height + lastRect.y;
                    WindowRect = new Rect(WindowRect.x, WindowRect.y, WindowRect.width, lastRect.height + lastRect.y);
                }
            }
            
            GUILayout.EndArea();

            GUI.DragWindow (new Rect (0,0,10000,10000));
        }
        
        protected override void OnDrawWindowClientArea(int instanceId) {}
        
        protected override void OnBecomeVisible() => this.SyncUIOverlay(base.OnBecomeVisible);
        protected override void OnBecomeInvisible() => this.SyncUIOverlay(base.OnBecomeInvisible);
        public Rect GetWindowRect() => WindowRect;
        
        #endregion PRIVATE
    }
}
