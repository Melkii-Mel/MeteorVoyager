using UnityEngine;

namespace Audio.Tools
{
    public class TemporaryPlayer : MonoBehaviour
    {
        private AudioSource _audioSource;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (!_audioSource.isPlaying) Destroy(gameObject);
        }
    }
}