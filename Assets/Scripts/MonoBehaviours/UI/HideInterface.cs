using UnityEngine;

namespace MonoBehaviours.UI
{
    public class HideInterface : MonoBehaviour
    {
        [SerializeField] private GameObject mainCamera;
        [SerializeField] private Vector3 displacementLength;
        [SerializeField] private float displacementTime;
        [SerializeField] private float hideCurveExponent;
        [SerializeField] private float showCurveExponent;

        private Transform _camT;
        private Vector3 _initialCamPos;
        private Vector3 _displacedCamPosY;
        private bool _hidden;
        private void Start()
        {
            _camT = mainCamera.transform;
            _initialCamPos = _camT.position;
            _displacedCamPosY = _initialCamPos + displacementLength;
        }

        public void SwitchState()
        {
            if (_hidden)
            {
                ShowGUI();
            }
            else
            {
                HideGUI();
            }
            _hidden = !_hidden;
        }

        private void HideGUI()
        {
            StartCoroutine(Mover.Move(_camT, _initialCamPos, _displacedCamPosY, displacementTime, hideCurveExponent));
        }
        
        private void ShowGUI()
        {
            StartCoroutine(Mover.Move(_camT, _displacedCamPosY, _initialCamPos, displacementTime, showCurveExponent));
        }
    }
}