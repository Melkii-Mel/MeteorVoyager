using System.Collections;
using MonoBehaviours.UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Animations.GUI
{
    public class ButtonAppearing : MonoBehaviour
    {
        [SerializeField] private float timeS;
        [SerializeField] private float speedAcceleration;
        [SerializeField] private float scaleAcceleration;
        private float TimePow => Mathf.Pow(2, speedAcceleration);
        private Transform _transform1;
        private Vector3 _initScale;
        private Vector3 _initPos;
        private Vector3 _offScreenPos;

        void Awake()
        {
            _transform1 = transform;
            
            _initScale = _transform1.localScale;
            _initPos = _transform1.position;
            _offScreenPos = new Vector3(Consts.LBorder, (Consts.UBorder + Consts.BBorder) / 2, -5f);
        }

        public void Open()
        {
            gameObject.SetActive(true);
            _transform1.position = _offScreenPos;
            StartCoroutine(Mover.Move(_transform1, _offScreenPos, _initPos, timeS, speedAcceleration));
            StartCoroutine(Mover.Resize(_transform1, Vector3.zero, _initScale, timeS, scaleAcceleration));
        }

        public void Close()
        {
            IEnumerator Closing()
            {
                yield return StartCoroutine(Mover.Move(_transform1, _initPos, _offScreenPos, timeS, speedAcceleration));
                gameObject.SetActive(false);
            }

            StartCoroutine(Closing());

        }

        // private IEnumerator Opening()
        // {
        //     var transform1 = transform;
        //     Vector3 localScale = _initialScale;
        //     transform1.localScale = Vector3.zero;
        //     float currentTime = 0;
        //     while (true)
        //     {
        //         yield return null;
        //         currentTime += Time.deltaTime;
        //         if (currentTime >= timeS)
        //         {
        //             break;
        //         }
        //         transform1.localScale = localScale * (Mathf.Pow(currentTime, TimePow) / timeS * sizeOnOverSizePercents);
        //     }
        //
        //     currentTime = backTrackTime;
        //     while (true)
        //     {
        //         yield return null;
        //         currentTime -= Time.deltaTime;
        //         if (currentTime <= 0)
        //         {
        //             break;
        //         }
        //         transform1.localScale =
        //             localScale + localScale * (Mathf.Pow(currentTime, TimePow) / backTrackTime * (sizeOnOverSizePercents - 1));
        //     }
        //
        //     transform.localScale = localScale;
        // }
        //
        // private IEnumerator Closing()
        // {
        //     var transform1 = transform;
        //     Vector3 localScale = _initialScale;
        //
        //     float currentTime = 0;
        //     while (true)
        //     {
        //         yield return null;
        //         currentTime += Time.deltaTime;
        //         if (currentTime >= backTrackTime)
        //         {
        //             break;
        //         }
        //         transform.localScale =
        //             localScale + localScale * ((sizeOnOverSizePercents - 1) * Mathf.Pow(currentTime, TimePow) / backTrackTime);
        //     }
        //
        //     currentTime = timeS;
        //     while (true)
        //     {
        //         yield return null;
        //         currentTime -= Time.deltaTime;
        //         if (currentTime <= 0)
        //         {
        //             break;
        //         }
        //         transform.localScale = localScale * (Mathf.Pow(currentTime, TimePow) / timeS * sizeOnOverSizePercents);
        //     }
        //
        //     transform.localScale = localScale;
        //     gameObject.SetActive(false);
        // }
    }
}