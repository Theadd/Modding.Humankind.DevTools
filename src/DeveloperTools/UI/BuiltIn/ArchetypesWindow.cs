﻿using Amplitude.Mercury.Overlay;
using UnityEngine;

namespace Modding.Humankind.DevTools.DeveloperTools.UI
{
    public class ArchetypesWindow : FloatingWindow_Archetypes
    {
        public static GUISkin Skin = null;
        
        protected override void Awake()
        {
            this.WindowStartupLocation = new Vector2(300f, 200f);
            this.Title = "Archetypes & Biases";
            base.Awake();
            this.Width = 720f;
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
            WindowUtils.DrawWindowHeader(this);
    }
}