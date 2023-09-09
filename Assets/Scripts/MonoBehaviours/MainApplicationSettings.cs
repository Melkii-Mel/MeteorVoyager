using UnityEngine;

namespace MonoBehaviours
{
    public class MainApplicationSettings : MonoBehaviour
    {
        void Start()
        {
            Application.targetFrameRate = 60;
        }
    }
}