using UnityEngine;

namespace MonoBehaviours.Interfaces
{
    public interface IDamageable
    {
        void TakeDamage(InfiniteInteger damage, Quaternion direction = default);
    }
}
