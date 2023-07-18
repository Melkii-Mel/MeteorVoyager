using MonoBehaviours.Interfaces;
using UnityEngine;
using static GameStatsNS.GameStats;

namespace MonoBehaviours
{
    public class UltracoinBehaviour : MonoBehaviour, IDamageable
    {
        [SerializeField] private float transformCoeff;
        [SerializeField] private float rotationCoeff;
        [SerializeField] private float knockbackPower;
        [SerializeField] private int startingHitsAmount;

        public void TakeDamage(InfiniteInteger damage)
        {
            if (startingHitsAmount < 1)
            {
                Destroy();
            }
            startingHitsAmount -= 1;

            transform.localScale *= 1.1f;
            InfiniteInteger deltaBalance = Calculator.CalculateDefaultCoinsAmount(damage).Pow(3f);
            MainGameStatsHolder.Currency.Balance += deltaBalance;
            Debug.Log(deltaBalance);

            transform.Translate(new Vector2(0, transformCoeff * Time.deltaTime * knockbackPower), Space.World);
        }

        private void Destroy()
        {

            Destroy(gameObject);
        }

        private void Update()
        {
            transform.Translate(new Vector2(0, -transformCoeff * Time.deltaTime), Space.World);
            transform.Rotate(new Vector3(rotationCoeff, rotationCoeff, rotationCoeff));
        }
    }
}
