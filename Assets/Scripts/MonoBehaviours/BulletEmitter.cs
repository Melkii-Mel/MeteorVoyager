using Assets.Scripts.GameStatsNameSpace;
using MeteorVoyager.Assets.Scripts.MonoBehaviours;
using UnityEngine;

namespace Assets.Scripts.MonoBehaviours
{
    public class BulletEmitter : MonoBehaviour
    {
        [SerializeField] GameObject emitter;
        [SerializeField] GameObject bullet;
        public void Shoot(int spreadPower = 0, float charging = 0)
        {
            if (charging > 0)
            {
                GameObject thisBullet = Instantiate(bullet, emitter.transform.position, gameObject.transform.rotation);
                Bullet bulletBullet = thisBullet.GetComponent<Bullet>();
                float chargingCoeff = charging * 3 * Mathf.Sqrt(TurretUpgrades.Instance.ChargeAttack);
                bulletBullet.chargedDamageCoefficient = chargingCoeff;
                bulletBullet.chargedPierceCoefficient = chargingCoeff;
                bulletBullet.isCharged = true;
                thisBullet.transform.localScale *= 3;
                thisBullet.GetComponent<TrailRenderer>().widthMultiplier *= 3;
                thisBullet.GetComponent<TrailRenderer>().time = 1.5f;

                return;
            }
            Quaternion rot = transform.rotation;
            rot.z += Random.Range(-(float)spreadPower * Mathf.Deg2Rad, (float)spreadPower * Mathf.Deg2Rad);
            Instantiate(bullet, emitter.transform.position, emitter.transform.rotation);
        }
    }
}