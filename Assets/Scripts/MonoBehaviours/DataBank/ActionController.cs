using System.Collections;
using GameStatsNS;
using MonoBehaviours.DataBank.Canvases;
using MonoBehaviours.UI;
using UnityEngine;
using Random = System.Random;

namespace MonoBehaviours.DataBank
{
    public class ActionController : MonoBehaviour
    {
        [SerializeField] private Controller controller;
        [SerializeField] [Range(0f, 1f)] private float chance = 0.1f;
        [SerializeField] private float chanceTickRateS = 10f;
        [SerializeField] private MessageCanvas messageCanvas;
        [SerializeField] private UpgradesCanvas upgradesCanvas;
        private readonly Random _random = new();
        
        private IDataBankCanvas _currentCanvas;

        private void OnEnable()
        {
            Relocation.OnRelocationEnd += RelocationEnd;
            controller.OnLeavingStayPosition += DestroyCanvas;
            StartCoroutine(MyUpdate());
        }

        private void OnDisable()
        {
            Relocation.OnRelocationEnd -= RelocationEnd;
            controller.OnLeavingStayPosition -= DestroyCanvas;
            StopCoroutine(MyUpdate());
        }
        
        
        private IEnumerator MyUpdate()
        {
            while (true)
            {
                yield return null;
                bool dataBankVisited = GameStats.MainGameStatsHolder.DataBankOthers.DataBankVisited;
                if (dataBankVisited)
                {
                    yield return new WaitForSeconds(chanceTickRateS);
                    continue;
                }
                if (_random.NextDouble() > chance) yield return new WaitForSeconds(chanceTickRateS);
                GameStats.MainGameStatsHolder.DataBankOthers.DataBankVisited = true;
                controller.Spawn();
                SelectAction();
            }
        }
        
        private void SelectAction()
        {
            if (GameStats.MainGameStatsHolder.Progression.GameStage < 4)
            {
                AddCanvasToController(messageCanvas);
                Init();
            }
            
            if (GameStats.MainGameStatsHolder.Currency.Data > 0)
            {
                AddCanvasToController(messageCanvas);
                if (TryInit()) return;
                
                AddCanvasToController(messageCanvas);
                Init();
            }
        }

        private void AddCanvasToController(IDataBankCanvas canvas)
        {
            _currentCanvas = Instantiate(canvas.GameObject, controller.transform).GetComponent<IDataBankCanvas>();
            _currentCanvas.CanvasPrefab.SetActive(false);
            _currentCanvas.OnExit += controller.StartDespawn;
            
            //did that for the camera that renders the canvas to not capture the main scene part
            _currentCanvas.Transform.Translate(100, 100, 0);
        }

        private void Init()
        {
            if (!TryInit())
            {
                controller.StartDespawn();
            }
        }

        private void DestroyCanvas(Controller controller1, Controller.DataBankBehaviourEventArgs args)
        {
            PopUpActivityController.ForceTrue = false;
            Destroy(_currentCanvas.GameObject);
        }

        private void EnableCanvas(Controller sender, Controller.DataBankBehaviourEventArgs args)
        {
            SetCanvasActive(true);
        }

        private void DisableCanvas(Controller sender, Controller.DataBankBehaviourEventArgs args)
        {
            SetCanvasActive(false);
        }

        private void SetCanvasActive(bool value)
        {
            _currentCanvas.CanvasPrefab.SetActive(value);
            PopUpActivityController.ForceTrue = value;
            if (value)
            {
                _currentCanvas.DataBankCircularTimer.StartTimer();
            }
        }

        private bool TryInit()
        {
            bool success =  _currentCanvas.Init();
            if (success)
            {
                controller.OnReachingStayPosition += EnableCanvas;
            }
            return success;
        }

        private void RelocationEnd(Relocation sender, Relocation.RelocationEventArgs args)
        {
            GameStats.MainGameStatsHolder.DataBankOthers.DataBankVisited = false;
        }
    }
}