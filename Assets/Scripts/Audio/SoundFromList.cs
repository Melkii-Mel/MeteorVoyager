using System;
using GameStatsNS;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Audio
{
    /// <summary>
    /// Used to play a random sound from a list
    /// </summary>
    public class SoundFromList : MonoBehaviour
    {
        [SerializeField] private AudioClip[] clips;
        [SerializeField][Range(0, 1)] private float pitchRandomizationStrength;
        public SoundFromList(AudioClip[] clips)
        {
            this.clips = clips;
        }

        public void PlayRandomSound(GameObject emptyObject)
        {
            float pitch = 1 + Random.Range(-pitchRandomizationStrength / 2, pitchRandomizationStrength / 2);
            AudioSource player = Instantiate(emptyObject).AddComponent<AudioSource>();
            player.pitch = pitch;
            player.PlayOneShot(clips[Random.Range(0, clips.Length)], volumeScale: GameStats.MainGameStatsHolder.Settings.SoundsVolume);
        }
    }
}