using System;
using System.Collections;
using Amplitude.Framework.Overlay;
using Amplitude.Mercury.Overlay;
using Amplitude.Mercury.UI;
using Amplitude.UI;
using Amplitude.UI.Interactables;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Modding.Humankind.DevTools.DeveloperTools.UI
{
    [RequireComponent(typeof(UITransform))]
    [RequireComponent(typeof(UIButton))]
    public class UIOverlay : MonoBehaviour
    {
        public UIButton Control { get; protected set; }
        
        public UITransform UITransform { get; protected set; }

        public PopupWindow Target { get; set; }

        public static bool DEBUG_DRAW_OVERLAY { get; set; } = false;
        
        private IEnumerator _resync;
        private float _syncInterval = 0.1f;
        private Rect _lastRect = Rect.zero;
        private static GameObject _container;
        public bool IsVisibleSelf { get; private set; } = true;
        
        public static GameObject Container
        {
            get
            {
                if (_container == null)
                {
                    var parentContainer = GameObject.Find("/WindowsRoot/SystemOverlays");
                    var count = parentContainer.transform.childCount;
                    
                    for (int i = count - 1; i >= 0; i--)
                    {
                        var child = parentContainer.transform.GetChild(i);

                        if (child.name == "UIOverlayContainer") {
                            Destroy(child.gameObject);
                        }
                    }
                    
                    _container = new GameObject("UIOverlayContainer");
                    _container.transform.parent = parentContainer != null ? parentContainer.transform : null;
                    _container.AddComponent<UITransform>();
                }

                return _container;
            }
        }

        public static UIOverlay Find(string uuid)
        {
            var t = Container.transform.Find(uuid);

            return (t == null) ? Create(uuid) : t.GetComponent<UIOverlay>();
        }

        public void Sync(PopupWindow target)
        {
            this.Target = target;

            if (this.Target == null || !Target.IsVisible)
            {
                _syncInterval = -1f;
                StopCoroutine(_resync);
                gameObject.transform.parent = null;
                Destroy(gameObject);
                return;
            }

            var rect = target.GetWindowPosition();
            _lastRect = rect;
            this.UITransform.X = rect.x;
            this.UITransform.Y = rect.y;
            this.UITransform.Width = rect.width;
            this.UITransform.Height = rect.height;
            this.UITransform.VisibleSelf = true;
            IsVisibleSelf = true;
        }
        
        private IEnumerator Resync()
        {
            while (_syncInterval != -1f)
            {
                if (Target != null)
                {
                    var targetVisible = Target.IsWindowVisible();
                    
                    if (targetVisible)
                    {
                        if (!Target.GetWindowPosition().Equals(_lastRect))
                        {
                            _syncInterval = 0.1f;
                            Sync(this.Target);
                        }
                        else
                        {
                            _syncInterval = 1.0f;
                        }

                        if (!IsVisibleSelf)
                        {
                            UITransform.VisibleSelf = true;
                            IsVisibleSelf = true;
                        }
                    }
                    else if (targetVisible != IsVisibleSelf)
                    {
                        UITransform.VisibleSelf = false;
                        IsVisibleSelf = false;
                        _syncInterval = 0.8f;
                    }
                }
                
                yield return new WaitForSeconds(_syncInterval);
            }
            
        }

        protected static UIOverlay Create(string uuid)
        {
            var go = new GameObject(uuid);
            go.transform.parent = Container.transform;
            var overlay = go.AddComponent<UIOverlay>();
            overlay.Setup();

            return overlay;
        }

        protected void Setup()
        {
            this.UITransform = GetComponent<UITransform>();
            this.Control = GetComponent<UIButton>();
            this.Control.LoadIfNecessary();

            if (DEBUG_DRAW_OVERLAY && gameObject.GetComponent<SquircleBackgroundWidget>() == null)
            {
                var canvas = gameObject.AddComponent<SquircleBackgroundWidget>();
                canvas.BackgroundColor = Color.clear;
                canvas.OuterBorderColor = Color.green;
                canvas.BorderColor = Color.clear;
                canvas.CornerRadius = 0f;
            }
            
            this.Control.MouseEnter -= OnMouseEventHandler;
            this.Control.MouseEnter += OnMouseEventHandler;
            this.Control.MouseLeave -= OnMouseEventHandler;
            this.Control.MouseLeave += OnMouseEventHandler;
        }

        public void OnMouseEventHandler(IUIControl control, Vector2 coords) 
        {
            if (this.Target != null)
            {
                _syncInterval = 0.1f;
                Sync(this.Target);
            }
        }

        public static void Unload()
        {
            var c = GameObject.Find("/WindowsRoot/SystemOverlays/UIOverlayContainer");
            if (c != null)
            {
                c.transform.parent = null;
                Destroy(c);
                _container = null;
            }
        }
        
        void Start()
        {
            _resync = Resync();
            Setup();
            StartCoroutine(_resync);
        }
    }

    public static class UIWindowExtensions
    {
        public static void SyncUIOverlay(this PopupWindow target, Action priorAction = null)
        {
            priorAction?.Invoke();
            UIOverlay.Find("UIOverlay" + target.GetInstanceID().ToString()).Sync(target);
        }

        public static Rect GetWindowPosition(this PopupWindow target)
        {
            if (target is IGetWindowRect customTarget)
            {
                return customTarget.GetWindowRect();
            }

            return target.WindowPosition;
        }
        
        public static bool IsWindowVisible(this PopupWindow target)
        {
            if (target is IGetWindowRect customTarget)
            {
                return customTarget.ShouldBeVisible && target.IsVisible;
            }

            return target.IsVisible;
        }
    }
}
