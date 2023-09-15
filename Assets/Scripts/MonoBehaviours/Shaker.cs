using System.Collections;
using UnityEngine;

namespace MonoBehaviours
{
    public static class Shaker
    {
        public static IEnumerator StartShaking(Transform objectTransform, float amplitude, float timeS, float frequencyS, float restorationTimeS = 0)
        {
            Vector3 initialPosition = objectTransform.position;
            Vector3 direction;
            float speed;

            void RandomizeDirection()
            {
                direction = new Vector3(Random.Range(-1f, 1), Random.Range(-1f, 1), Random.Range(-1f, 1)).normalized;
            }

            void RandomizeSpeed()
            {
                speed = Random.Range(frequencyS / 2, frequencyS);
            }

            void Restore()
            {
                objectTransform.position = initialPosition;
            }
            
            RandomizeDirection();
            RandomizeSpeed();
            for (float i = 0; i < timeS; i += Time.deltaTime)
            {
                yield return null;

                objectTransform.position += direction * speed;

                if (Vector3.Distance(initialPosition, objectTransform.position) > amplitude)
                {
                    while (Vector3.Distance(initialPosition, objectTransform.position) >
                           Vector3.Distance(initialPosition + direction, objectTransform.position))
                    {
                        RandomizeDirection();
                    }
                    RandomizeSpeed();
                }
            }

            if (restorationTimeS - Time.deltaTime <= 0)
            {
                Restore();
                yield break;
            }
            
            for (float i = 0; i < restorationTimeS; i += Time.deltaTime)
            {
                objectTransform.position =
                    initialPosition + (objectTransform.position - initialPosition) * (restorationTimeS - i);
                yield return null;
            }

            Restore();
        }
    }
}
