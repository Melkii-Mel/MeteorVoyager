using System;
using UnityEngine;

namespace ParticlesBehaviours
{
    public class AsteroidParticles : MonoBehaviour
    {
        [SerializeField] private ParticleSystem asteroidsParticleSystem;
        private Vector3 _prevPosition;
        private float _defaultSpeed;

        void Update()
        {
            var position = transform.position;
            float speedDelta = Vector3.Distance(_prevPosition, position) * 2;
            var mainModule = asteroidsParticleSystem.main;
            mainModule.startSpeed = speedDelta;
            ParticleSystem.VelocityOverLifetimeModule vol = asteroidsParticleSystem.velocityOverLifetime;
            _prevPosition = position;
        }
    }
}
