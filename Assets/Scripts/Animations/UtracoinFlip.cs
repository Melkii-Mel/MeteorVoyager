using UnityEngine;

namespace Animations
{
    public class UtracoinFlip : MonoBehaviour
    {
        [SerializeField] private GameObject ultracoin;
        public void Update()
        {
            ultracoin.transform.rotation = Quaternion.AngleAxis(1, ultracoin.transform.position);
        }
    }
}
