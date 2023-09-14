using System;
using UnityEngine;

namespace DevOnly
{
    public class ChangeFPS : MonoBehaviour
    {
        #if UNITY_EDITOR
        [SerializeField] private int targetFPS;
        [SerializeField] private bool active;

        private void Update()
        {
            if (!active) return;
            Application.targetFrameRate = targetFPS;
        }
        
        #endif
    }
}