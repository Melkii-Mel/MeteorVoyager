using System;
using UnityEngine;

namespace Animations.Upgrade
{
    public class UpgradeParticleSystem : MonoBehaviour
    {
        [SerializeField] private ParticleSystem thisSystem;

        void Update()
        {
            if (thisSystem.particleCount != 0) return;
            if (thisSystem.isEmitting) return;
            Destroy(gameObject);
        }
    }
}
