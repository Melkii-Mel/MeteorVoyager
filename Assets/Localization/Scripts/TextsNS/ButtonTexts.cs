using System;
using UnityEngine.Serialization;

namespace Localization.Scripts.TextsNS
{
    [Serializable]
    public class ButtonTexts
    {
        [FormerlySerializedAs("HintButtonText")] public string hintButtonText;
        #region Relocation
        [FormerlySerializedAs("RelocationScreen")] public string relocationScreen = "Скачок";
        [FormerlySerializedAs("ConfirmRelocation")] public string confirmRelocation = "СКАЧОК!";
        [FormerlySerializedAs("UnableToRelocate")] public string unableToRelocate = "Пока что совершнить скачок невозможно";
        #endregion
        #region Turret Upgrades
        [FormerlySerializedAs("PierceCountUpgrade")] public string pierceCountUpgrade = "Сила пробивания";
        [FormerlySerializedAs("ShotCooldown")] public string shotCooldown = "Перезарядка выстрела";
        [FormerlySerializedAs("ChargeAttack")] public string chargeAttack = "Заряженная атака";
        [FormerlySerializedAs("SpawnCooldown")] public string spawnCooldown = "Скорость появления астероидов";
        [FormerlySerializedAs("Damage")] public string damage = "Урон";
        #endregion
        #region MeteorUpgrades
        [FormerlySerializedAs("GlowingEnemiesSpawnRate")] public string glowingEnemiesSpawnRate = "Шанс появления мерцающих астероидов";
        [FormerlySerializedAs("ExplosivesAttacksTimeUpgrade")] public string explosivesAttacksTimeUpgrade = "Длительность разрывных атак";
        [FormerlySerializedAs("CoinMultiplier")] public string coinMultiplier = "Больше материи";
        [FormerlySerializedAs("CoinMultiplierTimeUpgrade")] public string coinMultiplierTimeUpgrade = "Длительность множителя материи";
        [FormerlySerializedAs("DamageMultiplierTimeUpgrade")] public string damageMultiplierTimeUpgrade = "Длительность множителя урона";
        #endregion
        #region DataUpgrades (NotImplemented)
        #endregion
    }
}
