using UnityEngine;

namespace Animations
{
    public class GlowingPulse : MonoBehaviour
    {
        [SerializeField] private float period;
        [SerializeField] private float deltaSize;
        [SerializeField] private GameObject glower;
        private Vector3 _standardScale;

        private float _currentTime;
        // Start is called before the first frame update
        void Start()
        {
            _standardScale = transform.localScale;
            glower.SetActive(true);
        }

        // Update is called once per frame
        void Update()
        {
            transform.localScale = _standardScale + Vector3.one * (deltaSize * (1 - _currentTime));
            if (_currentTime > period)
            {
                _currentTime = 0;
            }
            _currentTime += Time.deltaTime;
        }
    }
}
