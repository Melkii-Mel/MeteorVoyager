using MeteorVoyager.Assets.Localization.Scripts.TextsNS;
using UnityEngine;

namespace MeteorVoyager.Assets.Localization
{
    public class Runner : MonoBehaviour
    {
        void Start()
        {
            new Serializer().Serialize();
        }
    }
}
