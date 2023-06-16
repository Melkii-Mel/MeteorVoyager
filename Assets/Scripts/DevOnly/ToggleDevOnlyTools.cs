using UnityEngine;

namespace MeteorVoyager.Assets.Scripts.DevOnly
{
    public class ToggleDevOnlyTools : MonoBehaviour
    {
        [SerializeField] bool isDevOnlyToolsEnabled;
        [SerializeField] GameObject devOnlyTools;
        void Update()
        {
            devOnlyTools.SetActive(isDevOnlyToolsEnabled);
        }
    }
}