using System;
using UnityEngine;

namespace MonoBehaviours.UI
{
    public class SafeScreen : MonoBehaviour
    {
        private RectTransform _rectTransform;
        private Rect _safeAreaRect;
        private Rect _defaultRect;
        
        private void Awake()
        {
            _safeAreaRect = Screen.safeArea;
            _rectTransform = GetComponent<RectTransform>();
            _defaultRect = new Rect(0, 0, Screen.width, Screen.height);
        }

        public void EnableSafeArea()
        {
            try
            {
                SetRectComponents(_rectTransform, ref _safeAreaRect);
            }
            catch
            {
                Awake();
                EnableSafeArea();
            }
        }

        public void DisableSafeArea()
        {
            try
            {
                SetRectComponents(_rectTransform, ref _defaultRect);
            }
            catch
            {
                Awake();
                EnableSafeArea();
            }
        }

        void SetRectComponents(RectTransform rectTransform, ref Rect rect)
        {
            Vector2 anchorMin = rect.position;
            Vector2 anchorMax = rect.position + rect.size;
            anchorMin.x /= Screen.width;
            anchorMax.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.y /= Screen.height;
            rectTransform.anchorMin = anchorMin;
            rectTransform.anchorMax = anchorMax;
        }
    }
}