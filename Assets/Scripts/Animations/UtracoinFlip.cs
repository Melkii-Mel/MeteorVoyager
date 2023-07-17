using UnityEngine;

namespace MeteorVoyager.Assets.Scripts.Animations
{
    public class UtracoinFlip : MonoBehaviour
    {
        [SerializeField] GameObject ultracoin;
        public void Update()
        {
            ultracoin.transform.rotation = Quaternion.AngleAxis(1, ultracoin.transform.position);
        }
    }
}
