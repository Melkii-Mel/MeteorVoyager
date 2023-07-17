using System;

namespace MeteorVoyager.Assets.Localization.Scripts.TextsNS
{
    [Serializable]
    public class ButtonTexts
    {
        public string HintButtonText;
        #region Relocation
        public string RelocationScreen = "Скачок";
        public string ConfirmRelocation = "СКАЧОК!";
        public string UnableToRelocate = "Пока что совершнить скачок невозможно";
        #endregion
        #region Turret Upgrades
        public string PierceCountUpgrade = "Сила пробивания";
        public string ShotCooldown = "Перезарядка выстрела";
        public string ChargeAttack = "Заряженная атака";
        public string SpawnCooldown = "Скорость появления астероидов";
        public string Damage = "Урон";
        #endregion
        #region MeteorUpgrades
        public string GlowingEnemiesSpawnRate = "Шанс появления мерцающих астероидов";
        public string ExplosivesAttacksTimeUpgrade = "Длительность разрывных атак";
        public string CoinMultiplier = "Больше материи";
        public string CoinMultiplierTimeUpgrade = "Длительность множителя материи";
        public string DamageMultiplierTimeUpgrade = "Длительность множителя урона";
        #endregion
        #region DataUpgrades (NotImplemented)
        #endregion
    }
}
