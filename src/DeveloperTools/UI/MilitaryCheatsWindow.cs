
using System;
using Amplitude.Framework.Overlay;
using Amplitude.Mercury.Overlay;
using UnityEngine;

namespace Modding.Humankind.DevTools.DeveloperTools.UI
{
    public class FramerateWindow : FloatingWindow_Framerate
    {
        public static GUISkin Skin = null;
        
        protected override void Awake()
        {
            this.WindowStartupLocation = new Vector2(100f, 350f);
            this.Title = "Framerate";
            base.Awake();
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

    public class MilitaryCheatsWindow : FloatingWindow_MilitaryCheats
    {
        public static GUISkin Skin = null;
        
        protected override void Awake()
        {
            this.WindowStartupLocation = new Vector2(300f, 200f);
            this.Title = "Military Cheats";
            base.Awake();
            this.Width = 400f;
            // this.IsVisible = true;
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

        protected override void OnDrawWindowClientArea(int instanceId)
        {
            try {
                base.OnDrawWindowClientArea(instanceId);
            } catch (NullReferenceException e) {
                Loggr.Log("CATCHED NullReferenceException IN MilitaryCheatsWindow");
            }
        }
    }

    public class ResourcesUtilsWindow : FloatingWindow_ResourcesUtils
    {
        public static GUISkin Skin = null;
        
        protected override void Awake()
        {
            this.WindowStartupLocation = new Vector2(300f, 200f);
            this.Title = "Resources Utils";
            base.Awake();
            this.Width = 800f;
            // this.IsVisible = true;
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

    public class TechnologyUtilsWindow : FloatingWindow_TechnologyUtils
    {
        public static GUISkin Skin = null;
        
        protected override void Awake()
        {
            this.WindowStartupLocation = new Vector2(300f, 200f);
            this.Title = "Technology Utils";
            base.Awake();
            this.Width = 800f;
            // this.IsVisible = true;
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