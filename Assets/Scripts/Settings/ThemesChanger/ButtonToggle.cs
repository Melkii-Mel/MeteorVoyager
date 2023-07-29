using System;
using GameStatsNS;
using MonoBehaviours.Color_Themes;
using UnityEngine;
using UnityEngine.UI;

namespace Settings.ThemesChanger
{
    public class ButtonToggle : MonoBehaviour
    {
        [SerializeField] private Theme theme;
        private void Start()
        {
            GetComponent<Button>().interactable = GameStats.MainGameStatsHolder.Settings.Theme != theme;
            GetComponent<Button>().onClick.AddListener(ChangeAllButtonsActivity);
            ColorBlock colors = GetComponent<Button>().colors;
            colors.normalColor = GetComponent<Image>().material.color;
            GetComponent<Button>().colors = colors;
            GetComponent<Image>().material = null;
            if (theme == GameStats.MainGameStatsHolder.Settings.Theme) ChangeAllButtonsActivity();
        }

        private void ChangeAllButtonsActivity()
        {
            foreach (Transform objTransform in gameObject.GetComponentsInParent<Transform>()[1].GetComponentsInChildren<Transform>())
            {
                Debug.Log(objTransform);
                if (objTransform.TryGetComponent(out Button button))
                {
                    button.interactable = true;
                }
            }

            GameStats.MainGameStatsHolder.Settings.Theme = theme;
            gameObject.GetComponent<Button>().interactable = false;
            GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor =
                GetComponent<Button>().colors.normalColor;
            ColorBlock closeButtonColor = GameObject.Find("Close").GetComponent<Button>().colors;
            closeButtonColor.normalColor = GetComponent<Button>().colors.normalColor;
            GameObject.Find("Close").GetComponent<Button>().colors = closeButtonColor;
        }
    }
}
