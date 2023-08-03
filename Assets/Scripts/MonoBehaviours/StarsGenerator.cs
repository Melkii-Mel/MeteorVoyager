using System.Collections;
using GameStatsNS;
using UnityEngine;
using UnityEngine.Serialization;

namespace MonoBehaviours
{
    public class StarsGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject particles;
        [SerializeField] private Camera cam;
        [FormerlySerializedAs("delayTimeCoeffSF")] [SerializeField] private float coeffSf;
        private float _delayTimeCoeff;
        private bool _coroutineStarted;
        public static bool RelocationState = false;
        public static float RelocationDelayCoeff = 1;
        // Start is called before the first frame update
        private void Start()
        {
            if (coeffSf == 0) coeffSf = 1;
            UpdateCoefficient();
        }

        private void Update()
        {
            UpdateCoefficient();
            if (!_coroutineStarted && _delayTimeCoeff != 0 && !float.IsNaN(_delayTimeCoeff) && !float.IsPositiveInfinity(_delayTimeCoeff))
            {
                StartCoroutine(nameof(Generator));
                _coroutineStarted = true;
            }
            if (float.IsNaN(_delayTimeCoeff)  || float.IsInfinity(_delayTimeCoeff))
            {
                StopCoroutine(Generator());
                _coroutineStarted = false;
            }
        }
        public void UpdateCoefficient()
        {
            _delayTimeCoeff = coeffSf * RelocationDelayCoeff / GameStats.MainGameStatsHolder.Progression.GameStage;
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
                yield return new WaitForSeconds(_delayTimeCoeff);
            }
        }
    }
}