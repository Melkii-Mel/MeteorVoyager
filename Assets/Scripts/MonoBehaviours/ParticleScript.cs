using UnityEngine;

namespace MeteorVoyager.Assets.Scripts.MonoBehaviours
{
    public class ParticleScript : MonoBehaviour
    {
        [SerializeField] private float time;
        void Awake()
        {
            if (time == 0) time = 2;
        }

        void Update()
        {
            time -= Time.deltaTime;
            if (time < 0) Destroy(gameObject);
        }
    }
}