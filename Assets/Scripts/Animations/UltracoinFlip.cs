using MonoBehaviours;
using UnityEngine;

namespace Animations
{
    [RequireComponent(typeof(UltracoinBehaviour))]
    public class UltracoinFlip : MonoBehaviour
    {
        [SerializeField] private GameObject ultracoin;
        [SerializeField] private float rotCoeffOnHit;
        [SerializeField] private float startingRot;
        private float _rot;

        public void Start()
        {
            _rot = startingRot;
            GetComponent<UltracoinBehaviour>().OnHit += () => _rot *= rotCoeffOnHit; 
        }

        public void Update()
        {
            ultracoin.transform.Rotate(_rot, _rot, _rot);
        }
    }
}
