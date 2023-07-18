using MonoBehaviours.Interfaces;
using UnityEngine;

namespace MonoBehaviours
{
    public class ExplosionBehaviour : MonoBehaviour
    {
        private float _lifetime = 0.4f;

        private void Update()
        {
            _lifetime -= Time.deltaTime;
            if (_lifetime < 0)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            collision.gameObject.GetComponent<IDamageable>().TakeDamage(DamageCalculator.CalculateDefaultDamage());
        }
    }
}