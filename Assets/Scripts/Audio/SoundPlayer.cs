using System;
using MonoBehaviours;
using MonoBehaviours.Interfaces;
using UnityEngine;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundPlayer : MonoBehaviour
    {
        private AudioSource _audioSource;
        [SerializeField] private AudioClip impactClip;
        [SerializeField] private AudioClip shotClip;
        [SerializeField] private AudioClip destroyClip;

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

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            
        }

        public void PlayImpactSound(Enemy _)
        {
            _audioSource.PlayOneShot(impactClip);
        }

        public void PlayShotSound()
        {
            _audioSource.PlayOneShot(shotClip);
        }

        public void PlayDestroyClip(Enemy _)
        {
            _audioSource.PlayOneShot(destroyClip);
        }
    }
}
