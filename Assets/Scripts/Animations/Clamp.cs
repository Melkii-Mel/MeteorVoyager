using MeteorVoyager.Assets.Scripts.MonoBehaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeteorVoyager
{
    public class Clamp : MonoBehaviour
    {
        [SerializeField] Transform bone0l;
        [SerializeField] Transform bone0r;
        [SerializeField] Transform bone1l;
        [SerializeField] Transform bone1r;
        Quaternion bone0lRotationStart;
        Quaternion bone0rRotationStart;
        Quaternion bone1lRotationStart;
        Quaternion bone1rRotationStart;


        [SerializeField] float bone0coeff;
        [SerializeField] float bone1coeff;

        Quaternion[] stRots;
        Transform[] bones;

        float dynamicCoeff0 = 1;
        float dynamicCoeff1 = 1;

        public void Start()
        {
            bone0lRotationStart = bone0l.localRotation;
            bone0rRotationStart = bone0r.localRotation;
            bone1lRotationStart = bone1l.localRotation;
            bone1rRotationStart = bone1r.localRotation;

            stRots = new Quaternion[] { bone0lRotationStart, bone0rRotationStart, bone1lRotationStart, bone1rRotationStart };
            bones = new Transform[] { bone0l, bone0r, bone1l, bone1r };
            
            Player.OnShot += ClampDefault;
            Player.OnChargedShot += ClampCharged;

            GlobalTimer.AddAction(TickUnclamp);
        }

        public void Update()
        {
            bone0l.localRotation = bone0lRotationStart * Quaternion.Euler(0, 0, dynamicCoeff0);
            bone0r.localRotation = bone0rRotationStart * Quaternion.Euler(0, 0, -dynamicCoeff0);
            bone1l.localRotation = bone1lRotationStart * Quaternion.Euler(0, 0, dynamicCoeff1);
            bone1r.localRotation = bone1rRotationStart * Quaternion.Euler(0, 0, -dynamicCoeff1);
        }

        public void ClampDefault()
        {
            dynamicCoeff0 = bone0coeff;
            dynamicCoeff1 = bone1coeff;
        }
        public void ClampCharged()
        {
            dynamicCoeff0 = -bone0coeff * 2;
            dynamicCoeff1 = -bone1coeff * 2;
        }

        const float STEP_MULTIPLIER = 0.90f;
        public void TickUnclamp()
        {
            dynamicCoeff0 *= STEP_MULTIPLIER;
            dynamicCoeff1 *= STEP_MULTIPLIER;
        }
    }
}
