using UnityEngine;

namespace MonoBehaviours.Color_Themes
{
    public class MaterialRequester : MonoBehaviour
    {
        [SerializeField] private MaterialChanger changer;
        [SerializeField] private GameObject[] ignore;
        private void Start()
        {
            changer.SetCurrentMainMaterial(gameObject, ignore);
        }
    }
}