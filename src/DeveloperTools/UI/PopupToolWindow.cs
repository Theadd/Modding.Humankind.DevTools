﻿using System;
using System.Collections;
using UnityEngine;
using Amplitude.Framework.Overlay;
using Modding.Humankind.DevTools;

namespace Modding.Humankind.DevTools.DeveloperTools.UI
{
    public abstract class PopupToolWindow : PopupWindow, IToolWindow
    {
        public PopupToolWindow() => IsDraggable = true;

        public string TypeName { get; set; } = null;

        private bool ForceWriteAsVisible = false;

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
            PopupToolWindow popupWindow = this;
            
            yield return (object) base.Start();
            string playerPrefKey = popupWindow.GetPlayerPrefKey("IsVisible");
            if (PlayerPrefs.HasKey(playerPrefKey) && PlayerPrefs.GetInt(playerPrefKey) != 0)
                popupWindow.ShowWindow(true);
        }

        public virtual string GetPlayerPrefKey(string key)
        {
            if (TypeName == null)
                TypeName = GetType().Name;

            return "ToolWindow." + TypeName + "." + key;
        }

        // TODO: REMOVE
        public static string GetDerivedTypeName<T>() where T : PopupToolWindow
        {
            return typeof(T).Name;
        }

        public static bool WasVisible<T>() where T : PopupToolWindow
        {
            return PlayerPrefs.GetInt("ToolWindow." + typeof(T).Name + ".IsVisible", 0) == 1;
        }

        public virtual void OnWritePlayerPreferences()
        {
            PlayerPrefs.SetInt(GetPlayerPrefKey("IsVisible"), ForceWriteAsVisible || IsVisible ? 1 : 0);
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
        
        private static T Open<T>() where T : PopupToolWindow
        {
            var window = DevTools.GetGameObject()?.GetComponent<T>() ?? DevTools.GetGameObject()?.AddComponent<T>();
            
            if (window != null)
                window.ShowWindow(true);
            
            return (T) window;
        }
        
        public static void Open<T>(Action<T> callback) where T : PopupToolWindow
        {
            if (!UIController.IsGUILoaded)
                UIController.OnGUIHasLoaded += () => callback.Invoke(Open<T>());
            else
                callback.Invoke(Open<T>());
        }

        public virtual void Close(bool saveVisibilityStateBeforeClosing = false)
        {
            if (saveVisibilityStateBeforeClosing && IsVisible)
                ForceWriteAsVisible = true;
            
            ShowWindow(false);
            Destroy(this);
        }
    }
}