using UnityEngine;

namespace MonoBehaviours
{
    public class ParticleScript : MonoBehaviour
    {
        [SerializeField] private float time;

        private void Awake()
        {
            if (time == 0) time = 2;
        }

        private void Update()
        {
            time -= Time.deltaTime;
            if (time < 0) Destroy(gameObject);
        }
    }
}