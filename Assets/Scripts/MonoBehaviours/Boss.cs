using System.Collections;
using UnityEngine;

namespace MeteorVoyager.Assets.Scripts.MonoBehaviours
{
    public class Boss : MonoBehaviour
    {
        [SerializeField] GameObject smallBullet;
        [SerializeField] GameObject largeBullet;
        [SerializeField] GameObject emitter;

        // Start is called before the first frame update
        void Start()
        {

        }

        IEnumerator Actions()
        {
            float time = 0;
            while (time < 2)
            {
                time += Time.deltaTime;
            }
            while (time < 3)
            {
                transform.Translate(0, -2 * Time.deltaTime, 0);
                time += Time.deltaTime;
                yield return null;
            }
            time = 0;
            while (time < 2)
            {
                time += Time.deltaTime;
                yield return null;
            }
            time = 0;
            for (int i = 0; i < 8; i++)
            {
                Quaternion angle = new();
                angle.z = gameObject.transform.rotation.z - 30 * Mathf.Deg2Rad;
                Instant();
                yield return new WaitForSeconds(1 / 4f);
                angle.z += 20 * Mathf.Deg2Rad;
                Instant();
                yield return new WaitForSeconds(1 / 4f);
                angle.z += 20 * Mathf.Deg2Rad;
                Instant();
                yield return new WaitForSeconds(1 / 2f);

                void Instant()
                {
                    Instantiate(smallBullet, emitter.transform.position, angle);
                }
            }



            yield return new WaitForSeconds(int.MaxValue);
        }
    }
}