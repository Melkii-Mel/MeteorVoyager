using UnityEngine;

namespace Animations
{
    /// <summary>
    /// Scales the light range proportionally to the local scaling of the object.
    /// Make sure that the object scales equally on all coordinates (X, Y, and Z) for the scaling to affect the light range correctly.
    /// </summary>
    public class LightScale : MonoBehaviour
    {
        private float _scaleX;
        private float _lightRange;
        private Light _thisLight;
        private void Awake()
        {
            _scaleX = transform.lossyScale.x;
            _thisLight = GetComponent<Light>();
            _lightRange = _thisLight.range;
        }

        private void Update()
        {
            _thisLight.range = _lightRange / _scaleX * transform.lossyScale.x;
        }
    }
}