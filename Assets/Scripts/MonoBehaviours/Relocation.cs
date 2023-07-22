using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using static GameStatsNS.GameStats;

namespace MonoBehaviours
{
    public class Relocation : MonoBehaviour
    {
        public class RelocationEventArgs
        {
            public InfiniteInteger DataAmount { get; }

            public RelocationEventArgs(InfiniteInteger dataAmount)
            {
                DataAmount = dataAmount;
            }
        }
        
        private const float FIRST_PHASE_TIME = 5;
        private const float LAST_PHASE_TIME = 6;
        [SerializeField] private Text relocationText;
        [SerializeField] private Player player;
        [SerializeField] private GameObject canvas;
        [SerializeField] private GameObject cam;
        [SerializeField] private EnemySpawner spawner;
        [FormerlySerializedAs("ButtonDisablerField")] [SerializeField] private GameObject buttonDisablerField;

        #region events

        public delegate void RelocationEventHandler(Relocation sender, RelocationEventArgs args);

        public static event RelocationEventHandler OnRelocation;

        #endregion

        public void Start()
        {
            StartCoroutine(Calculator());
        }

        /// <summary>
        /// Used to show how many units of data you will recieve upon relocation
        /// </summary>
        private IEnumerator Calculator()
        {
            for (; ; )
            {
                InfiniteInteger value = CalculateAmountOfData();
                relocationText.text = string.Format(Texts.OtherTexts.DataUponRelocation, value);
                yield return null;
            }
        }
        public void Relocate()
        {
            StartCoroutine(Relocator());
        }

        private IEnumerator Relocator()
        {
            Vector3 cameraPos = cam.transform.position;
            Vector3 canvasPos = canvas.transform.position;
            InfiniteInteger dataAmount = CalculateAmountOfData();

            SetDisablerFieldActive(true);
            player.DisableContol();
            spawner.StopEnemiesSpawning();
            EnableTrailsForStars();
            SetStarsGeneratorStateOnRelocationState();
            RemoveAllEnemies();
            for (float i = 0; i < FIRST_PHASE_TIME; i += Time.deltaTime)
            {
                StarsBehaviour.SpeedCoefficientDuringRelocation = 10 * Mathf.Pow(2, i + 1);
                StarsGenerator.RelocationDelayCoeff = Mathf.Pow(2, 5 - i);
                Shake(coeff: i);
                yield return new WaitForEndOfFrame();
            }

            for (float i = 0; i < LAST_PHASE_TIME; i += Time.deltaTime)
            {
                Shake(coeff: i);
                yield return new WaitForEndOfFrame();
            }

            GiveData(dataAmount);
            ResetGameStats();
            RestoreAll();
            SetDisablerFieldActive(false);
            AfterRelocation();
            OnRelocation?.Invoke(this, new RelocationEventArgs(dataAmount));

            #region local functions
            void RemoveAllEnemies()
            {
                for (int i = EnemySpawner.Enemies.Count - 1; i >= 0; i--)
                {
                    GameObject enemy = EnemySpawner.Enemies[i];
                    EnemySpawner.Enemies.Remove(enemy);
                    Destroy(enemy);
                }
            }
            void ResetPosition()
            {
                cam.transform.position = cameraPos;
                canvas.transform.position = canvasPos;
            }
            void Shake(float coeff = 1)
            {
                ResetPosition();
                Vector2 displace = new(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                displace.x *= coeff;
                displace.y *= coeff;
                Vector2 camdisplace = displace;
                camdisplace.x /= 800f;
                camdisplace.y /= 1600f;
                camdisplace *= 5;
                cam.transform.Translate(camdisplace);
                canvas.transform.Translate(-displace);
            }
            void GiveData(InfiniteInteger value)
            {
                MainGameStatsHolder.Currency.Data += value;
            }
            void RestoreAll()
            {
                ResetPosition();
                player.EnableContol();
                spawner.StartEnemiesSpawning();
                StarsBehaviour.SpeedCoefficientDuringRelocation = 1;
                StarsGenerator.RelocationDelayCoeff = 1;
                StarsBehaviour.TrailsEnabled = false;
                StarsGenerator.RelocationState = false;
            }
            void EnableTrailsForStars()
            {
                StarsBehaviour.TrailsEnabled = true;
            }
            void SetStarsGeneratorStateOnRelocationState()
            {
                StarsGenerator.RelocationState = true;
            }
            void SetDisablerFieldActive(bool value)
            {
                buttonDisablerField.SetActive(value);
            }
            void ResetGameStats()
            {
                MainGameStatsHolder.TurretUpgrades.ResetDeletableValues();
                MainGameStatsHolder.MeteorUpgrades.ResetDeletableValues();
                MainGameStatsHolder.Timers.ResetDeletableValues();
                MainGameStatsHolder.Currency.Balance = 0;
                MainGameStatsHolder.SerializeAll();
            }
            #endregion
        }

        private InfiniteInteger CalculateAmountOfData()
        {
            return MainGameStatsHolder.Currency.Balance.Pow(0.5f);
        }
    }
}