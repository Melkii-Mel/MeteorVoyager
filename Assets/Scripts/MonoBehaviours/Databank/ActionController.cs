using System;
using UnityEngine;

namespace MonoBehaviours.DataBank
{
    public class ActionController : MonoBehaviour
    {
        [SerializeField] private Behaviour behaviour;

        private void OnEnable()
        {
            behaviour.OnReachingStayPosition += SelectAction;
        }

        private void OnDisable()
        {
            behaviour.OnReachingStayPosition -= SelectAction;
        }

        private void SelectAction(Behaviour sender, Behaviour.DataBankBehaviourEventArgs args)
        {
            
        }
    }
}