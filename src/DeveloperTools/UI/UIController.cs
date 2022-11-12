using System;
using Amplitude.Framework.Overlay;
using UnityEngine;
using Amplitude.Mercury.Presentation;
using HarmonyLib;
using Modding.Humankind.DevTools.Core;

namespace Modding.Humankind.DevTools.DeveloperTools.UI
{
    public class UIController
    {
        private static Amplitude.Mercury.UI.Helpers.DataUtils _dataUtils;
        private static Amplitude.Mercury.UI.Helpers.TextUtils _textUtils;
        private static Amplitude.Mercury.UI.Helpers.GameUtils _gameUtils;
        private static Amplitude.Mercury.UI.Helpers.FormatUtils _formatUtils;
        private static Amplitude.Mercury.UI.UIManager _service;

        public static event Action OnGUIHasLoaded;

        public static bool IsGUILoaded { get; protected set; } = false;

        public static GUISkin DefaultSkin { get; set; }

        public static Amplitude.Mercury.UI.Helpers.DataUtils DataUtils => _dataUtils ?? 
            (_dataUtils = (Amplitude.Mercury.UI.Helpers.DataUtils) R.Fields.DataUtilsField.GetValue(null));

        public static Amplitude.Mercury.UI.Helpers.TextUtils TextUtils => _textUtils ??
            (_textUtils = (Amplitude.Mercury.UI.Helpers.TextUtils) R.Fields.TextUtilsField.GetValue(null));

        public static Amplitude.Mercury.UI.Helpers.GameUtils GameUtils => _gameUtils ?? 
            (_gameUtils = (Amplitude.Mercury.UI.Helpers.GameUtils) R.Fields.GameUtilsField.GetValue(null));

        public static Amplitude.Mercury.UI.Helpers.FormatUtils FormatUtils => _formatUtils ?? 
            (_formatUtils = (Amplitude.Mercury.UI.Helpers.FormatUtils) R.Fields.FormatUtilsField.GetValue(null));

        public static Amplitude.Mercury.UI.UIManager Service => _service
            ? _service
            : (_service =
                Amplitude.Framework.Services.GetService<Amplitude.Mercury.UI.IUIService>() as
                    Amplitude.Mercury.UI.UIManager);

        public static float TooltipDelay
        {
            get => Service.TooltipDelay;
            set => Service.TooltipDelay = value;
        }
        
        public static bool IsAmplitudeUIVisible
        {
            get => Service.IsUiVisible;
            set => Service.UpdateUIVisibility(value);
        }
        
        public static bool AreTooltipsVisible
        {
            get => Service.AreTooltipsVisible;
            set => Service.AreTooltipsVisible = value;
        }
        
        public static bool IsCameraSequenceRunning => Service?.IsCameraSequenceRunning ?? false;

        public static bool AnyPendingSequence => Presentation.PresentationCameraSequenceController?.AnyPendingSequence ?? false;
        
        public static bool GodMode
        {
            get => Amplitude.Mercury.Presentation.GodMode.Enabled;
            set => AccessTools.PropertySetter(typeof(GodMode), "Enabled")?.Invoke(null, new object[] { value }); 
        }

        public static string GetLocalizedTitle(Amplitude.StaticString uiMapperName, string defaultValue = null) => 
            DataUtils.TryGetLocalizedTitle(uiMapperName, out string title) ? title : defaultValue;

        public static string GetLocalizedDescription(Amplitude.StaticString uiMapperName) => 
            DataUtils?.GetLocalizedDescription(uiMapperName) ?? uiMapperName.ToString();

        public static void Initialize()
        {
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
                UnityEngine.Object.Destroy(window);
            }
        }

        public static void OnceGUIHasLoaded(Action action)
        {
            if (!IsGUILoaded)
                OnGUIHasLoaded += () => action.Invoke();
            else
                action.Invoke();
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
