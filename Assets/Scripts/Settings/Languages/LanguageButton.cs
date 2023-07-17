using MeteorVoyager.Assets.Localization.Scripts;
using MeteorVoyager.Assets.Scripts.GameStatsNameSpace;
using UnityEngine;
using UnityEngine.UI;

public class LanguageButton : MonoBehaviour
{
    [SerializeField] Languages language;

    public void Start()
    {
        GetComponent<Button>().onClick.AddListener(ChangeLanguage);
    }
    private void ChangeLanguage()
    {
        GameStats.UpdateTexts(language);
    }
}
