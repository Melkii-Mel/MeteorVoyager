using Animations;
using MonoBehaviours.Interfaces;
using UnityEngine;
using static GameStatsNS.GameStats;

namespace MonoBehaviours
{
    [RequireComponent(typeof(UltracoinFlip))]
    public class UltracoinBehaviour : MonoBehaviour, IDamageable
    {
        [SerializeField] private float transformCoeff;
        [SerializeField] private float knockbackPower;
        [SerializeField] private int startingHitsAmount;
        [SerializeField] private float scaleOnHit = 1.05f;
        
        #region events

        public delegate void HitEventHandler();
        
        public event HitEventHandler OnHit;
        #endregion

        public void TakeDamage(InfiniteInteger damage)
        {
            OnHit?.Invoke();
            if (startingHitsAmount < 1)
            {
                Destroy();
            }
            startingHitsAmount -= 1;

            transform.localScale *= scaleOnHit;
            InfiniteInteger deltaBalance = Calculator.CalculateDefaultCoinsAmount(damage).Pow(3f);
            MainGameStatsHolder.Currency.Balance += deltaBalance;

            transform.Translate(new Vector2(0, transformCoeff * Time.deltaTime * knockbackPower), Space.World);
        }

        private void Destroy()
        {

            Destroy(gameObject);
        }

        private void Update()
        {
            transform.Translate(new Vector2(0, -transformCoeff * Time.deltaTime), Space.World);
        }

        public void TakeDamage(InfiniteInteger damage, Quaternion direction = default)
        {
            throw new System.NotImplementedException();
        }
    }
}
