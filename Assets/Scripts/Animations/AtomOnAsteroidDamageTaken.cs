using System.Collections;
using GameStatsNS;
using MonoBehaviours;
using MonoBehaviours.UI;
using UnityEngine;

namespace Animations
{
    public class AtomOnAsteroidDamageTaken : MonoBehaviour
    {
        [SerializeField] private float sizeMultiplier;
        [SerializeField] private Vector2 destinationPoint;
        [SerializeField] private float flightTime;
        [SerializeField] private float speedCurveExponent;
        [SerializeField] private float sizeCurveExponent;
        [SerializeField] private GameObject flyingObject;
        [SerializeField] private int maxAmount;

        private int _thingsAmount;
        
        private void OnEnable()
        {
            Enemy.OnAnyEnemyDamageTaken += SpawnAtom;
        }

        private void OnDisable()
        {
            Enemy.OnAnyEnemyDamageTaken -= SpawnAtom;
        }

        private void SpawnAtom(Enemy sender)
        {
            if (!GameStats.MainGameStatsHolder.Settings.ParticlesEnabled) return;
            
            Transform senderTransform = sender.transform;
            Vector3 position = senderTransform.position;
            
            if (_thingsAmount > Random.Range(1, maxAmount))
            {
                return;
            }
            
            GameObject obj = Instantiate(flyingObject, senderTransform.position, senderTransform.rotation);
            obj.transform.localScale *= sizeMultiplier;
            _thingsAmount++;
            
            IEnumerator Resize()
            {
                yield return Mover.Resize(obj.transform, obj.transform.localScale, Vector3.zero, flightTime, sizeCurveExponent);
            }
            IEnumerator Move()
            {
                yield return Mover.Move(obj.transform, position, destinationPoint, flightTime, speedCurveExponent);
                Destroy(obj);
                _thingsAmount--;
            }
            
            StartCoroutine(Move());
            StartCoroutine(Resize());
        }
    }
}