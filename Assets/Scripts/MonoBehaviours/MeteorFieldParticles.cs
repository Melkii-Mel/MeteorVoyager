using System.Collections;
using UnityEngine;
using static UnityEngine.ParticleSystem;
using static GameStatsNS.GameStats;

namespace MonoBehaviours
{
    public class MeteorFieldParticles : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(nameof(Updater));
        }

        private IEnumerator Updater()
        {
            while (true)
            {
                int cm = MainGameStatsHolder.Currency.Balance.Exponent;
                var particles = GetComponent<ParticleSystem>();
                EmissionModule ps = particles.GetComponent<ParticleSystem>().emission;
                if (cm == 1)
                {
                    ps.rateOverTime = 10;
                }
                else if (cm == 2)
                {
                    ps.rateOverTime = 20;
                }
                else
                {
                    ps.rateOverTime = 40;
                }
                yield return new WaitForSeconds(1);
            }
        }
    }
}