using UnityEngine;

namespace Animations.Atom
{
    public class Spin : MonoBehaviour
    {
        [SerializeField] private float rotationX;
        [SerializeField] private float rotationY;
        [SerializeField] private float rotationZ;
        [SerializeField] private float coeff;
        private Vector3 _rotation;
        private const int FPS = 60;
        private void Start()
        {
            _rotation = new Vector3(rotationX, rotationY, rotationZ) * (Random.Range(1, 1.5f) * coeff * FPS);
        }

        private void Update()
        {
            transform.Rotate(_rotation * Time.deltaTime);
        }
    }
}