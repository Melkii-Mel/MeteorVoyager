using System;
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
        [SerializeField]
        private AudioClip[] clips;

        public SoundFromList(AudioClip[] clips)
        {
            this.clips = clips;
        }

        public void PlayRandom(GameObject emptyObject)
        {
            AudioSource player = Instantiate(emptyObject).AddComponent<AudioSource>();
            player.PlayOneShot(clips[Random.Range(0, clips.Length)]);
        }
    }
}