using UnityEngine;

namespace Localization.Scripts
{
    public class Runner : MonoBehaviour
    {
        private void Start()
        {
            new Serializer().Serialize();
        }
    }
}
