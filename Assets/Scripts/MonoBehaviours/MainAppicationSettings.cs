using UnityEngine;

namespace MonoBehaviours
{
    public class MainAppicationSettings : MonoBehaviour
    {
        void Start()
        {
            Application.targetFrameRate = 30;
        }
    }
}
