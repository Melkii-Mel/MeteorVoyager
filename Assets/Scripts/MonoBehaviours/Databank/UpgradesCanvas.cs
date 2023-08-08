using System;
using UnityEngine;

namespace MonoBehaviours.DataBank
{
    public class UpgradesCanvas : MonoBehaviour
    {
        [SerializeField] private GameObject upgradeCanvas;
        [SerializeField] private GameObject timer;

        private Behaviour _dataBankBehaviour;
        
        public void Init(Behaviour dataBankBehaviour)
        {
            _dataBankBehaviour = dataBankBehaviour;
            Timer dataBankTimer = upgradeCanvas.GetComponentInChildren<Timer>();
            dataBankTimer.OnTimeEnd += DataBankTimerTimeEnd;
            dataBankTimer.StartTimer();
            
        }

        private void DataBankTimerTimeEnd(Timer sender, Timer.DataBankTimerEventArgs args)
        {
            sender.OnTimeEnd -= DataBankTimerTimeEnd;
            _dataBankBehaviour.StartDespawn();
            Destroy(upgradeCanvas);
        }
    }
}