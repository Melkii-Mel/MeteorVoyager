using UnityEngine;
using static GameStatsNS.GameStats;

namespace MonoBehaviours
{
    public class BulletEmitter : MonoBehaviour
    {
        [SerializeField] private GameObject bullet;
        [SerializeField] private GameObject emitter;
        public void Shoot(int spreadPower = 0, float charging = 0)
        {
            if (charging > 0)
            {
                GameObject chargedBullet = Instantiate(bullet, emitter.transform.position, gameObject.transform.rotation);
                Bullet bulletBullet = chargedBullet.GetComponent<Bullet>();
                float chargingCoeff = charging * 3 * Mathf.Sqrt(MainGameStatsHolder.TurretUpgrades.ChargeAttack);
                bulletBullet.chargedDamageCoefficient = chargingCoeff;
                bulletBullet.chargedPierceCoefficient = chargingCoeff;
                bulletBullet.isCharged = true;
                chargedBullet.transform.localScale *= 3;
                chargedBullet.GetComponent<TrailRenderer>().widthMultiplier *= 3;
                chargedBullet.GetComponent<TrailRenderer>().time *= 2.5f;
                chargedBullet.GetComponent<Bullet>().EmitterTransform = transform;
                return;
            }
            Quaternion rot = transform.rotation;
            rot.z += Random.Range(-(float)spreadPower * Mathf.Deg2Rad, spreadPower * Mathf.Deg2Rad);
            GameObject thisBullet = Instantiate(bullet, emitter.transform.position, emitter.transform.rotation);
            thisBullet.GetComponent<Bullet>().EmitterTransform = transform;
        }
    }
}