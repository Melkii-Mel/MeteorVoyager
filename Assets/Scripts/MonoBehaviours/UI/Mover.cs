using System;
using System.Collections;
using UnityEngine;

namespace MonoBehaviours.UI
{
    public static class Mover
    {
        /// <summary>
        /// Moves an object from position A to position B for C seconds;
        /// </summary>
        /// 
        /// <param name="obj">transform of an object that will be moved</param>
        /// <param name="initPos">initial position</param>
        /// <param name="finalPos">final position</param>
        /// <param name="time">time in seconds</param>
        /// <param name="exponent">curve exponent</param>
        /// <param name="ignoreExceptions">if it's true, all exceptions are ignored (useful when the gameObject might be destroyed)</param>
        public static IEnumerator Move(Transform obj, Vector3 initPos, Vector3 finalPos, float time, float exponent, bool ignoreExceptions = true)
        {
            void Action(float i)
            {
                float percentage = Mathf.Pow(i / time, Mathf.Pow(2, exponent - 1));
                Vector3 pos = initPos + percentage * (finalPos - initPos);
                obj.position = pos;
            }
            
            yield return Iterate(Action, time, ignoreExceptions);
            obj.position = finalPos;
        }

        /// <summary>
        /// Changes size of an object from size A to size B for C seconds;
        /// </summary>
        /// <param name="obj">transform of an object which size will be changed</param>
        /// <param name="initSize">initial size</param>
        /// <param name="finalSize">final size</param>
        /// <param name="time">time in seconds</param>
        /// <param name="exponent">curve exponent</param>
        /// <param name="ignoreExceptions">if it's true, all exceptions are ignored (useful when the gameObject might be destroyed)</param>
        public static IEnumerator Resize(Transform obj, Vector3 initSize, Vector3 finalSize, float time,
            float exponent, bool ignoreExceptions = true)
        {
            void Action(float i)
            {
                float percentage = Mathf.Pow(i / time, Mathf.Pow(2, exponent - 1));
                Vector3 size = initSize + percentage * (finalSize - initSize);
                obj.localScale = size;
            }

            yield return Iterate(Action, time, ignoreExceptions);
            obj.localScale = finalSize;
        }

        private static IEnumerator Iterate(Action<float> action, float time, bool ignoreExceptions)
        {
            for (float i = 0; i < time; i += Time.deltaTime)
            {
                yield return null;
                try
                {
                    action(i);
                }
                catch (Exception e)
                {
                    if (ignoreExceptions) continue; //ignored
                    Debug.LogException(e);
                }
            }
        }
    }
}