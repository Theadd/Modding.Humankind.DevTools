using System;
using System.Collections;
using UnityEngine;
using Amplitude.Framework.Overlay;
using Modding.Humankind.DevTools;

namespace Modding.Humankind.DevTools.DeveloperTools.UI
{
    public abstract class FloatingToolWindow : PopupWindow, IToolWindow
    {
        public FloatingToolWindow() => IsDraggable = true;

        public string TypeName { get; set; } = null;

        protected virtual void OnApplicationQuit() => OnWritePlayerPreferences();

        protected override void OnBecomeInvisible()
        {
            base.OnBecomeInvisible();
            OnWritePlayerPreferences();
        }

        public virtual void SetWindowPosition(float x, float y)
        {
            float posX = Math.Min(x, Screen.width - 50f);
            float posY = Math.Min(y, Screen.height - 50f);

            var r = GetWindowRect();
            SetWindowRect(new Rect(posX, posY, r.width, r.height));
        }

        protected override void OnBecomeVisible()
        {
            OnReadPlayerPreferences();
            
            PlayerPrefs.SetInt(GetPlayerPrefKey("IsVisible"), 1);
            base.OnBecomeVisible();
        }

        protected override void OnDrawWindowClientArea(int instanceId) { }
        
        protected override IEnumerator Start()
        {
            FloatingToolWindow floatingWindow = this;
            
            yield return (object) base.Start();
            string playerPrefKey = floatingWindow.GetPlayerPrefKey("IsVisible");
            if (PlayerPrefs.HasKey(playerPrefKey) && PlayerPrefs.GetInt(playerPrefKey) != 0)
                floatingWindow.ShowWindow(true);
        }

        public virtual string GetPlayerPrefKey(string key)
        {
            if (TypeName == null)
                TypeName = GetType().Name;

            return "FloatingToolWindow." + TypeName + "." + key;
        }

        public virtual void OnWritePlayerPreferences()
        {
            PlayerPrefs.SetInt(GetPlayerPrefKey("IsVisible"), IsVisible ? 1 : 0);
            PlayerPrefs.SetFloat(GetPlayerPrefKey("X"), GetWindowRect().x);
            PlayerPrefs.SetFloat(GetPlayerPrefKey("Y"), GetWindowRect().y);
        }

        public virtual void OnReadPlayerPreferences()
        {
            string prefKeyX = GetPlayerPrefKey("X");
            string prefKeyY = GetPlayerPrefKey("Y");
            
            if (ShouldRestoreLastWindowPosition && PlayerPrefs.HasKey(prefKeyX) && PlayerPrefs.HasKey(prefKeyY))
                SetWindowPosition(PlayerPrefs.GetFloat(prefKeyX), PlayerPrefs.GetFloat(prefKeyY));
        }
        
        public abstract Rect GetWindowRect();
        public abstract void SetWindowRect(Rect rect);
        public abstract bool ShouldBeVisible { get; }
        public abstract bool ShouldRestoreLastWindowPosition { get; }
        
        private static T Open<T>() where T : FloatingToolWindow
        {
            var window = DevTools.GetGameObject()?.GetComponent<T>() ?? DevTools.GetGameObject()?.AddComponent<T>();
            
            if (window != null)
                window.ShowWindow(true);
            
            return (T) window;
        }
        
        public static void Open<T>(Action<T> callback) where T : FloatingToolWindow
        {
            if (!UIManager.IsGUILoaded)
                UIManager.OnGUIHasLoaded += () => callback.Invoke(Open<T>());
            else
                callback.Invoke(Open<T>());
        }

        public virtual void Close()
        {
            ShowWindow(false);
            Destroy(this);
        }
    }
}