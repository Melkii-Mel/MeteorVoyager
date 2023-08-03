using System.Collections;
using GameStatsNS.GameStatsTypes.Upgrades;
using UnityEngine;
using UnityEngine.UI;

namespace MonoBehaviours.UpgradesNS
{
    [RequireComponent(typeof(Button))]
    public class UpgradeButton : MonoBehaviour
    {
        [SerializeField] private GameObject buttonObject;
        [SerializeField] private float indentationCoefficient = -0.19f;
        private int _restorationTimeFrames = 10;
        private UpgradesButtonActions _uba;
        private bool _isUpgradeButton;

        private void Start()
        {
            _isUpgradeButton = TryGetComponent(out _uba);
            GetComponent<Button>().onClick.AddListener(StartIndentation);
        }

        private void StartIndentation()
        {
            StartCoroutine(Press());
        }

        private const float FRAME_TIME_FOR_60_FPS = 1 / 60f;

        private IEnumerator Press()
        {
            void Translate(float coeff)
            {
                buttonObject.transform.Translate(new Vector3(0, coeff));
            }
            float ic;
            if (!_isUpgradeButton || _uba.CheckIfCanUpgrade())
            {
                ic = indentationCoefficient;
            }
            else
            {
                ic = indentationCoefficient / 3;
            }
            Translate(ic);
            yield return new WaitForSeconds(FRAME_TIME_FOR_60_FPS);
            for (int i = 0; i < _restorationTimeFrames; i++)
            {
                Translate(-ic / _restorationTimeFrames);
                yield return new WaitForSeconds(FRAME_TIME_FOR_60_FPS);
            }
        }
    }
}
