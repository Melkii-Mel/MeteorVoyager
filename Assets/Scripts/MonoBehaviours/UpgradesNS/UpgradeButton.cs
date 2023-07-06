using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace MeteorVoyager.Assets.Scripts.MonoBehaviours.UpgradesNS
{
    public class UpgradeButton : MonoBehaviour
    {
        [SerializeField] GameObject buttonObject;
        [SerializeField] float indentationCoefficient = -0.19f;
        int restorationTimeFrames = 10;

        void Start()
        {
            GetComponent<Button>().onClick.AddListener(StartIndentation);
        }

        void StartIndentation()
        {
            StartCoroutine(Indentate());
        }

        private const float FRAME_TIME_FOR_60FPS = 1 / 60f;
        IEnumerator Indentate()
        {
            void Translate(float coeff)
            {
                buttonObject.transform.Translate(new Vector3(0, coeff));
                gameObject.transform.Translate(new Vector3(0, coeff));
            }
            float ic;
            if (gameObject.GetComponent<UpgradesButtonActions>() != null && gameObject.GetComponent<UpgradesButtonActions>().CheckIfCanUpgrade())
            {
                ic = indentationCoefficient;
            }
            else
            {
                ic = indentationCoefficient / 3;
            }
            Translate(ic);
            yield return new WaitForSeconds(FRAME_TIME_FOR_60FPS);
            for (int i = 0; i < restorationTimeFrames; i++)
            {
                Translate(-ic / restorationTimeFrames);
                yield return new WaitForSeconds(FRAME_TIME_FOR_60FPS);
            }
        }
    }
}
