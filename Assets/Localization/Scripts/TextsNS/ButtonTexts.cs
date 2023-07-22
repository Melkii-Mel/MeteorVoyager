using System;

namespace Localization.Scripts.TextsNS
{
    [Serializable]
    public class ButtonTexts
    {
        public string HintButtonText { get; set; } = "text";
        #region Relocation
        public string RelocationScreen  { get; set; } = "text";
        public string ConfirmRelocation  { get; set; } = "text";
        public string UnableToRelocate  { get; set; } = "text";
        #endregion
        #region Turret Upgrades
        public string PierceCountUpgrade  { get; set; } = "text";
        public string ShotCooldown  { get; set; } = "text";
        public string ChargeAttack  { get; set; } = "text";
        public string SpawnCooldown  { get; set; } = "text";
        public string Damage  { get; set; } = "text";
        #endregion
        #region MeteorUpgrades
        public string GlowingEnemiesSpawnRate  { get; set; } = "text";
        public string ExplosivesAttacksTimeUpgrade  { get; set; } = "text";
        public string CoinMultiplier  { get; set; } = "text";
        public string CoinMultiplierTimeUpgrade  { get; set; } = "text";
        public string DamageMultiplierTimeUpgrade  { get; set; } = "text";
        #endregion
        #region DataUpgrades

        public string BossSpawnChance { get; set; } = "text";
        public string ForceField { get; set; } = "text";
        public string UltracoinSpawnChance { get; set; } = "text";
        public string ScreenExplosion { get; set; } = "text";
        public string Multishot { get; set; } = "text";

        #endregion
    }
}
