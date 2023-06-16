using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MeteorVoyager.Assets.Scripts.MonoBehaviours
{
    public class ButtonsDisabledEnabler : MonoBehaviour
    {
        [SerializeField] List<Button> buttons;
        [SerializeField] List<GameObject> popUps;
        List<bool> states = new();
        bool PopUpEnabled;
        void Update()
        {
            if (!PopUpEnabled)
            {
                states = new();
                for (int i = 0; i < buttons.Count; i++)
                {
                    states.Add(buttons[i].interactable);
                }
            }
            foreach (var popUp in popUps)
            {
                if (popUp.activeSelf)
                {
                    ToggleAllButtons(false);
                    PopUpEnabled = true;
                    goto end;
                }
            }
            PopUpEnabled = false;
            ToggleAllButtons(true);
        end:;
        }

        void ToggleAllButtons(bool state)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                Button button = buttons[i];
                if (state == false)
                {
                    button.interactable = state;
                }
                else
                {
                    button.interactable = states[i];
                }
            }
        }
    }
}