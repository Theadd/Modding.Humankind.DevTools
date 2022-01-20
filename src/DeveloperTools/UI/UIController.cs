using System;
using Amplitude.Framework.Overlay;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Modding.Humankind.DevTools.DeveloperTools.UI
{
    public class UIManager
    {
        public static event Action OnGUIHasLoaded;

        public static bool IsGUILoaded { get; protected set; } = false;
        
        public static GUISkin GameSkin { get; set; }
        
        public static GUISkin Skin { get; set; }
        
        public static GUISkin PinnedSkin { get; set; }
        
        public static GUISkin DefaultSkin { get; set; }

        public static Amplitude.Mercury.UI.UIManager Service => _service
            ? _service
            : (_service =
                Amplitude.Framework.Services.GetService<Amplitude.Mercury.UI.IUIService>() as
                    Amplitude.Mercury.UI.UIManager);

        private static Amplitude.Mercury.UI.UIManager _service;

        public static void Initialize()
        {
            FindGUISkinResources();
            DefaultSkin = DevTools.Assets.Load<GUISkin>("GenericUISkin");
            
            // Unload any previous UIOverlay remaining on scene
            Unload();
            
            InvokeOnGUIHasLoaded();
        }
        
        public static T GetWindow<T>(bool createAsFallback = true) where T : PopupWindow
        {
            var window = DevTools.GetGameObject()?.GetComponent<T>();

            if (window == null && createAsFallback)
                window = DevTools.GetGameObject()?.AddComponent<T>();

            return (T) window;
        }

        public static void ShowWindow<T>(bool visible = true) where T : PopupWindow => GetWindow<T>().ShowWindow(visible);
        
        public static void HideWindow<T>() where T : PopupWindow => ShowWindow<T>(false);
        
        public static void CloseWindow<T>() where T : PopupWindow
        {
            var window = DevTools.GetGameObject().GetComponent<T>();
            
            if (window != null)
            {
                window.ShowWindow(false);
                Object.Destroy(window);
            }
        }

        private static void FindGUISkinResources()
        {
            foreach(var skin in Resources.FindObjectsOfTypeAll(typeof(GUISkin)) as GUISkin[]) {
                switch (skin.name)
                {
                    case "GameSkin":
                        GameSkin = skin;
                        break;
                    case "PopupWindowStyles":
                        Skin = skin;
                        break;
                    case "PopupWindowStyles+Pinned":
                        PinnedSkin = skin;
                        break;
                }
            }
        }

        protected static void InvokeOnGUIHasLoaded()
        {
            IsGUILoaded = true;
            OnGUIHasLoaded += Dummy;
            if (!BepInEx.Utility.TryDo(OnGUIHasLoaded, out Exception ex))
                Loggr.Log(ex);
            OnGUIHasLoaded -= Dummy;
        }
        
        private static void Dummy() { }

        private static void Unload()
        {
            UIOverlay.Unload();
        }
    }
}
