using UnityEngine;

namespace MeteorVoyager.Assets.Scripts.MonoBehaviours
{
    public class StarsBehaviour : MonoBehaviour
    {
        public static float speedCoefficientDuringRelocation = 1;
        public static bool trailsEnabled = false;
        void Start()
        {
            Invoke(nameof(Die), 5);
        }

        private void Update()
        {
            GetComponent<TrailRenderer>().enabled = trailsEnabled;
            transform.Translate(new Vector2(0, -0.03f) * (speedCoefficientDuringRelocation * Time.deltaTime * transform.localScale));
        }

        void Die()
        {
            Destroy(gameObject);
        }
    }
}