using System;
using UnityEngine;

namespace Animations.Atom
{
    public class Spin : MonoBehaviour
    {
        [SerializeField] private float rotationX;
        [SerializeField] private float rotationY;
        [SerializeField] private float rotationZ;
        [SerializeField] private float coeff;

        private void Update()
        {
            transform.Rotate(rotationX * coeff, rotationY * coeff, rotationZ * coeff);
        }
    }
}