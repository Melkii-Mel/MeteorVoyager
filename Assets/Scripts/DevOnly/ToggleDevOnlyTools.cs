using UnityEngine;

namespace DevOnly
{
    public class ToggleDevOnlyTools : MonoBehaviour
    {
        [SerializeField] private bool isDevOnlyToolsEnabled;
        [SerializeField] private GameObject devOnlyTools;

        private void Update()
        {
            devOnlyTools.SetActive(isDevOnlyToolsEnabled);
        }
    }
}