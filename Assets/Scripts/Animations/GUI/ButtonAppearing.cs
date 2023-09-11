using System.Collections;
using UnityEngine;

namespace Animations.GUI
{
    public class ButtonAppearing : MonoBehaviour
    {
        [SerializeField] private float timeS;
        [SerializeField] private float sizeOnOverSizePercents;
        [SerializeField] private float backTrackTime;
        [SerializeField] private float acceleration;
        private float TimePow => Mathf.Pow(2, acceleration);
        private Vector3 _initialScale;

        void Awake()
        {
            _initialScale = transform.localScale;
        }

        public void Open()
        {
            gameObject.SetActive(true);
            StartCoroutine(Opening());
        }

        public void Close()
        {
            StartCoroutine(Closing());
        }

        private IEnumerator Opening()
        {
            var transform1 = transform;
            Vector3 localScale = _initialScale;
            transform1.localScale = Vector3.zero;
            float currentTime = 0;
            while (true)
            {
                yield return null;
                currentTime += Time.deltaTime;
                if (currentTime >= timeS)
                {
                    break;
                }
                transform1.localScale = localScale * (Mathf.Pow(currentTime, TimePow) / timeS * sizeOnOverSizePercents);
            }

            currentTime = backTrackTime;
            while (true)
            {
                yield return null;
                currentTime -= Time.deltaTime;
                if (currentTime <= 0)
                {
                    break;
                }
                transform1.localScale =
                    localScale + localScale * (Mathf.Pow(currentTime, TimePow) / backTrackTime * (sizeOnOverSizePercents - 1));
            }

            transform.localScale = localScale;
        }

        private IEnumerator Closing()
        {
            var transform1 = transform;
            Vector3 localScale = _initialScale;

            float currentTime = 0;
            while (true)
            {
                yield return null;
                currentTime += Time.deltaTime;
                if (currentTime >= backTrackTime)
                {
                    break;
                }
                transform.localScale =
                    localScale + localScale * ((sizeOnOverSizePercents - 1) * Mathf.Pow(currentTime, TimePow) / backTrackTime);
            }

            currentTime = timeS;
            while (true)
            {
                yield return null;
                currentTime -= Time.deltaTime;
                if (currentTime <= 0)
                {
                    break;
                }
                transform.localScale = localScale * (Mathf.Pow(currentTime, TimePow) / timeS * sizeOnOverSizePercents);
            }

            transform.localScale = localScale;
            gameObject.SetActive(false);
        }
    }
}