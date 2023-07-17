using System;

namespace MeteorVoyager.Assets.Localization.Scripts.TextsNS
{
    [Serializable]
    public class ButtonTexts
    {
        public string HintButtonText;
        #region Relocation
        public string RelocationScreen = "������";
        public string ConfirmRelocation = "������!";
        public string UnableToRelocate = "���� ��� ���������� ������ ����������";
        #endregion
        #region Turret Upgrades
        public string PierceCountUpgrade = "���� ����������";
        public string ShotCooldown = "����������� ��������";
        public string ChargeAttack = "���������� �����";
        public string SpawnCooldown = "�������� ��������� ����������";
        public string Damage = "����";
        #endregion
        #region MeteorUpgrades
        public string GlowingEnemiesSpawnRate = "���� ��������� ��������� ����������";
        public string ExplosivesAttacksTimeUpgrade = "������������ ��������� ����";
        public string CoinMultiplier = "������ �������";
        public string CoinMultiplierTimeUpgrade = "������������ ��������� �������";
        public string DamageMultiplierTimeUpgrade = "������������ ��������� �����";
        #endregion
        #region DataUpgrades (NotImplemented)
        #endregion
    }
}
