using System;
using GameStatsNS;
using MonoBehaviours;
using MonoBehaviours.Interfaces;
using UnityEngine;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundPlayer : MonoBehaviour
    {
        private AudioSource _audioSource;
        [SerializeField] private ClipHolder impactClip;
        [SerializeField] private ClipHolder shotClip;
        [SerializeField] private ClipHolder destroyClip;

        private void OnEnable()
        {
            Enemy.OnAnyEnemyDestroy += PlayDestroyClip;
            Player.OnShot += PlayShotSound;
            Enemy.OnAnyEnemyDamageTaken += PlayImpactSound;
        }

        private void OnDisable()
        {
            Enemy.OnAnyEnemyDestroy -= PlayDestroyClip;
            Player.OnShot -= PlayShotSound;
            Enemy.OnAnyEnemyDamageTaken -= PlayImpactSound;
        }

        private void Update()
        {
            _audioSource.volume = GameStats.MainGameStatsHolder.Settings.SoundsVolume;
        }

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            
        }

        private void PlayImpactSound(Enemy _)
        {
            PlayIfNotOnCooldown(impactClip);
        }

        private void PlayShotSound()
        {
            PlayIfNotOnCooldown(shotClip);
        }

        private void PlayDestroyClip(Enemy _)
        {
            PlayIfNotOnCooldown(destroyClip);
        }

        private void PlayIfNotOnCooldown(ClipHolder clipHolder)
        {
            if (clipHolder.OnCooldown)
            {
                return;
            }
            _audioSource.PlayOneShot(clipHolder.Clip);
        }

        [Serializable]
        private class ClipHolder
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
}
