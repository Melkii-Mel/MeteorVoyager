using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeteorVoyager.Assets.Scripts.MonoBehaviours
{
    public class StarsGenerator : MonoBehaviour
    {
        [SerializeField] GameObject particles;
        [SerializeField] Camera cam;
        [SerializeField] float delayTime;
        private float actualDelayTime;
        float delayTimeCoeff;
        bool coroutineStarted;
        public List<StarsBehaviour> stars;
        public static bool relocationState = false;
        public static float relocationDelayCoeff = 1;
        // Start is called before the first frame update
        void Start()
        {
            if (delayTime == 0) delayTime = 1;
            UpdateCoefficient();
        }

        // Update is called once per frame
        void Update()
        {
            UpdateCoefficient();
            if (!coroutineStarted && delayTimeCoeff != 0 && delayTimeCoeff != float.NaN && delayTimeCoeff != float.PositiveInfinity)
            {
                StartCoroutine(nameof(Generator));
                coroutineStarted = true;
            }
            if (delayTimeCoeff == float.NaN || delayTimeCoeff == float.PositiveInfinity)
            {
                StopCoroutine(Generator());
                coroutineStarted = false;
            }
        }
        public void UpdateCoefficient()
        {
            delayTimeCoeff = delayTime * relocationDelayCoeff / (Enemy.maxEnemyHealth.GetExponent() + 1);
        }

        private IEnumerator Generator()
        {
            for (; ; )
            {
                int y = Random.Range(Screen.height / 2, Screen.height / 2 * 3);
                if (relocationState)
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
                yield return new WaitForSeconds(delayTimeCoeff);
            }
        }
    }
}