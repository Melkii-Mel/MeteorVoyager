using static MeteorVoyager.Assets.Scripts.GameStatsNameSpace.GameStats;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace MeteorVoyager.Assets.Scripts.MonoBehaviours
{
    public class Relocation : MonoBehaviour
    {
        [SerializeField] Text relocationText;
        [SerializeField] Player player;
        [SerializeField] GameObject canvas;
        [SerializeField] GameObject cam;
        [SerializeField] EnemySpawner spawner;


        public void Start()
        {
            StartCoroutine(Calculator());
        }

        /// <summary>
        /// Used to show how much data you will get after relocation
        /// </summary>
        IEnumerator Calculator()
        {
            for (; ; )
            {
                InfiniteInteger value = CalculateAmountOfData();
                relocationText.text = $"After relocation \nyou will receive \n{value} units of data";
                yield return null;
            }
        }
        public void Relocate()
        {
            StartCoroutine(Relocator());
        }
        IEnumerator Relocator()
        {
            Vector2 cameraPos = cam.transform.position;
            Vector2 canvasPos = canvas.transform.position;

            void ResetPos()
            {
                cam.transform.position = cameraPos;
                canvas.transform.position = canvasPos;
            }

            void Shake(float coeff = 1)
            {
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
            InfiniteInteger value = CalculateAmountOfData();
            player.DisableContol();
            spawner.StopEnemiesSpawning();
            StarsBehaviour.trailsEnabled = true;
            StarsGenerator.relocationState = true;
            for (int i = EnemySpawner.Enemies.Count - 1; i >= 0; i--)
            {
                GameObject enemy = EnemySpawner.Enemies[i];
                EnemySpawner.Enemies.Remove(enemy);
                Destroy(enemy);
            }
            for (float i = 0; i < 5; i += Time.deltaTime)
            {
                ResetPos();
                StarsBehaviour.speedCoefficientDuringRelocation = 10 * Mathf.Pow(2, i + 1);
                StarsGenerator.relocationDelayCoeff = Mathf.Pow(2, 5 - i);
                Shake(coeff: i);
                yield return new WaitForEndOfFrame();
            }
            float time = 6;
            while (time > 0)
            {
                ResetPos();
                Shake(time);
                time -= Time.deltaTime;
                yield return null;
            }

            MainGameStatsHolder.Currency.Data = value;
            ResetPos();
            player.EnableContol();
            spawner.StartEnemiesSpawning();
            StarsBehaviour.speedCoefficientDuringRelocation = 1;
            StarsGenerator.relocationDelayCoeff = 1;
            StarsBehaviour.trailsEnabled = false;
            StarsGenerator.relocationState = false;
            yield return null;

        }
        InfiniteInteger CalculateAmountOfData()
        {
            InfiniteInteger result = MainGameStatsHolder.Currency.Balance.Pow(0.5f);
            return result;
        }
    }
}