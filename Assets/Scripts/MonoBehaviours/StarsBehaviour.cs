using UnityEngine;

namespace MonoBehaviours
{
    public class StarsBehaviour : MonoBehaviour
    {
        public static float SpeedCoefficientDuringRelocation = 1;
        public static bool TrailsEnabled = false;

        private void Start()
        {
            Invoke(nameof(Die), 5);
        }

        private void Update()
        {
            GetComponent<TrailRenderer>().enabled = TrailsEnabled;
            transform.Translate(new Vector2(0, -0.03f) * (SpeedCoefficientDuringRelocation * Time.deltaTime * transform.localScale));
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}