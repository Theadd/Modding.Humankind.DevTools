
using Amplitude.Mercury.Overlay;
using UnityEngine;

namespace Modding.Humankind.DevTools.DeveloperTools.UI
{
    public class MilitaryCheatsWindow : FloatingWindow_MilitaryCheats
    {
        public static GUISkin Skin = null;
        
        protected override void Awake()
        {
            this.WindowStartupLocation = new Vector2(300f, 200f);
            this.Title = "Military Cheats";
            base.Awake();
            this.Width = 400f;
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
        
        protected override void OnBecomeVisible() => this.SyncUIOverlay(base.OnBecomeVisible);
        protected override void OnBecomeInvisible() => this.SyncUIOverlay(base.OnBecomeInvisible);
    }
}
