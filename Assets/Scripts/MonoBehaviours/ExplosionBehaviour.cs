using UnityEngine;

namespace MeteorVoyager.Assets.Scripts.MonoBehaviours
{
    public class ExplosionBehaviour : MonoBehaviour
    {
        private float lifetime = 0.4f;

        private void Update()
        {
            lifetime -= Time.deltaTime;
            if (lifetime < 0)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            collision.gameObject.GetComponent<Enemy>().DealDamage(DamageCalculator.CalculateDefaultDamage());
        }
    }
}