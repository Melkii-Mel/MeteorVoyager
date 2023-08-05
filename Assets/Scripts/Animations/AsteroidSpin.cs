using UnityEngine;
using Random = UnityEngine.Random;

namespace Animations
{
    public class AsteroidSpin : MonoBehaviour
    {
        private float _rotX = 1;
        private float _rotY = 1;
        private float _rotZ = 1;
        private const float MULTIPLIER = 7;
    
        void Start()
        {
            _rotY *= Random.Range(-3f, 3f);
            _rotX *= Random.Range(-3f, 3f);
            _rotZ *= Random.Range(-3f, 3f);
        }

        void Update()
        {
            transform.Rotate(_rotX, _rotY, _rotZ);
        }
    }
}
