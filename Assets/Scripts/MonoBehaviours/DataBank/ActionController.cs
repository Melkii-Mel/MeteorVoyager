using System.Collections;
using GameStatsNS;
using MonoBehaviours.DataBank.Actions;
using UnityEngine;
using Random = System.Random;

namespace MonoBehaviours.DataBank
{
    public class ActionController : MonoBehaviour
    {
        [SerializeField] private Behaviour behaviour;
        [SerializeField] [Range(0f, 1f)] private float chance = 0.1f;
        [SerializeField] private float chanceTickRateS = 10f;
        [SerializeField] private MessageCanvas messageCanvas;
        [SerializeField] private UpgradesCanvas upgradesCanvas;
        private readonly Random _random = new();



        private IDataBankCanvas _currentCanvas;

        private void OnEnable()
        {
            Relocation.OnRelocationEnd += RelocationEnd;
            StartCoroutine(MyUpdate());
        }

        private void OnDisable()
        {
            Relocation.OnRelocationEnd -= RelocationEnd;
            StopCoroutine(MyUpdate());
        }
        
        
        private IEnumerator MyUpdate()
        {
            while (true)
            {
                if(GameStats.MainGameStatsHolder.DataBankOthers.DataBankVisited) yield break;
                if (_random.NextDouble() > chance) yield return new WaitForSeconds(chanceTickRateS);
                GameStats.MainGameStatsHolder.DataBankOthers.DataBankVisited = true;
                behaviour.Spawn();
                SelectAction();
            }
        }
        
        private void SelectAction()
        {
            if (GameStats.MainGameStatsHolder.Progression.GameStage < 4)
            {
                _currentCanvas = messageCanvas;
                Init();
            }
            
            if (GameStats.MainGameStatsHolder.Currency.Data > 0)
            {
                _currentCanvas = upgradesCanvas;
                Init();
            }
        }

        private void Init()
        {
            if (!_currentCanvas.Init())
            {
                behaviour.StartDespawn();
            }
        }

        private void RelocationEnd(Relocation sender, Relocation.RelocationEventArgs args)
        {
            GameStats.MainGameStatsHolder.DataBankOthers.DataBankVisited = false;
        }
    }
}