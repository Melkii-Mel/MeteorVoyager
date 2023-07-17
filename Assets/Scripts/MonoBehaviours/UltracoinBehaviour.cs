using MeteorVoyager.Assets.Scripts.GameStatsNameSpace;
using MeteorVoyager.Assets.Scripts.MonoBehaviours.Interfaces;
using UnityEngine;

namespace MeteorVoyager.Assets.Scripts.MonoBehaviours
{
    public class UltracoinBehaviour : MonoBehaviour, IDamageable
    {
        [SerializeField] float transformCoeff;
        [SerializeField] float rotationCoeff;
        [SerializeField] float knockbackPower;
        [SerializeField] int startingHitsAmount;

        public void TakeDamage(InfiniteInteger damage)
        {
            if (startingHitsAmount < 1)
            {
                Destroy();
            }
            startingHitsAmount -= 1;

            transform.localScale *= 1.1f;
            InfiniteInteger deltaBalance = Calculator.CalculateDefaultCoinsAmount(damage).Pow(3f);
            GameStats.MainGameStatsHolder.Currency.Balance += deltaBalance;
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
