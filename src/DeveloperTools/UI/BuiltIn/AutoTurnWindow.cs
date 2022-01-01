﻿using Amplitude.Mercury.Overlay;
using UnityEngine;

namespace Modding.Humankind.DevTools.DeveloperTools.UI
{
    public class AutoTurnWindow : FloatingWindow_AutoTurn
    {
        public static GUISkin Skin = null;
        
        protected override void Awake()
        {
            this.WindowStartupLocation = new Vector2(300f, 200f);
            this.Title = "Auto Turn";
            base.Awake();
            this.Width = 800f;
            this.SetAnchorPosition(new Vector2(300f, 200f));
        }

        void OnGUI() {
            if (IsVisible)
            {
                GUI.skin = Skin != null ? Skin : UIManager.DefaultSkin;
                DrawWindow();
            }
        }

        protected override void OnDrawWindowTitle(int instanceId) =>
            WindowUtils.DrawWindowTitleBar(this);
    }
}