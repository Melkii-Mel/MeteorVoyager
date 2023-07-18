using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace MonoBehaviours
{
    public class StarsGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject particles;
        [SerializeField] private Camera cam;
        [FormerlySerializedAs("delayTimeCoeffSF")] [SerializeField] private float delayTimeCoeffSf;
        private float _delayTimeCoeff;
        private bool _coroutineStarted;
        public List<StarsBehaviour> stars;
        public static bool RelocationState = false;
        public static float RelocationDelayCoeff = 1;
        // Start is called before the first frame update
        private void Start()
        {
            if (delayTimeCoeffSf == 0) delayTimeCoeffSf = 1;
            UpdateCoefficient();
        }

        // Update is called once per frame
        private void Update()
        {
            UpdateCoefficient();
            if (!_coroutineStarted && _delayTimeCoeff != 0 && _delayTimeCoeff != float.NaN && _delayTimeCoeff != float.PositiveInfinity)
            {
                StartCoroutine(nameof(Generator));
                _coroutineStarted = true;
            }
            if (_delayTimeCoeff == float.NaN || _delayTimeCoeff == float.PositiveInfinity)
            {
                StopCoroutine(Generator());
                _coroutineStarted = false;
            }
        }
        public void UpdateCoefficient()
        {
            _delayTimeCoeff = delayTimeCoeffSf * RelocationDelayCoeff / (EnemyHealthAndSpawnDelayCoefficientsCalculator.CalculateHealthCoefficient().Exponent + 1);
        }

        private IEnumerator Generator()
        {
            for (; ; )
            {
                int y = Random.Range(Screen.height / 2, Screen.height / 2 * 3);
                if (RelocationState)
                {
                    y += Screen.height;
                }
                int x = Random.Range(0, Screen.width);
                Vector3 position = cam.ScreenToWorldPoint(new(x, y));
                position.z = 50;
                GameObject star = Instantiate(particles, position, new Quaternion());
                float scale = Random.Range(0.5f, 2);
                star.transform.localScale *= new Vector2(scale, scale);
                stars.Add(star.GetComponent<StarsBehaviour>());
                yield return new WaitForSeconds(_delayTimeCoeff);
            }
        }
    }
}