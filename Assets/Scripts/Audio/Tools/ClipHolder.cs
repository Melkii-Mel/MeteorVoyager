using System;
using UnityEngine;

namespace Audio.Tools
{
    [Serializable]
    public class ClipHolder
    {
        [SerializeField] private AudioClip clip;
        [SerializeField] private float cooldownMS;
        private DateTime _lastGetting;
        public AudioClip Clip
        {
            get
            {
                _lastGetting = DateTime.Now;
                return clip;
            }
        }
        /// <summary>
        /// Cooldown sets when Clip getter is used
        /// </summary>
        public bool OnCooldown => (DateTime.Now - _lastGetting).TotalSeconds < cooldownMS / 1000;

        public ClipHolder()
        {
            _lastGetting = DateTime.Now;
        }
    }
}