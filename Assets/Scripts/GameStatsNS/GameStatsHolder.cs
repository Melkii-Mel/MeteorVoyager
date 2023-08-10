using System;
using GameStatsNS.GameStatsTypes;
using SerializationLibrary;
using UnityEngine;
using GameStatsNS.GameStatsTypes.Upgrades;

namespace GameStatsNS
{
    public class GameStatsHolder : SerializablesHolder
    {
        public Currency Currency { get; private set; }
        public DataUpgradesGameStats DataUpgrades { get; private set; }
        public MeteorUpgrades MeteorUpgrades { get; private set; }
        public Progression Progression { get; private set; }
        public SettingsGameStats Settings { get; private set; }
        public Timers Timers { get; private set; }
        public TurretUpgrades TurretUpgrades { get; private set; }
        public DataBankUpgrades DataBankUpgrades { get; private set; }
        public DataBankOthers DataBankOthers { get; private set; }
        public GameStatsHolder(int serializablesIndex, Func<bool> runningCondition, float delayS) : base(serializablesIndex, runningCondition, delayS, Application.persistentDataPath)
        {
            Currency = CreateSerializable<Currency>();
            DataUpgrades = CreateSerializable<DataUpgradesGameStats>();
            MeteorUpgrades = CreateSerializable<MeteorUpgrades>();
            Progression = CreateSerializable<Progression>();
            Settings = CreateSerializable<SettingsGameStats>();
            Timers = CreateSerializable<Timers>();
            TurretUpgrades = CreateSerializable<TurretUpgrades>();
            DataBankUpgrades = CreateSerializable<DataBankUpgrades>();
            DataBankOthers = CreateSerializable<DataBankOthers>();
        }
        public void ResetAllSerialization()
        {
            Currency = ResetSerializable<Currency>();
            DataUpgrades = ResetSerializable<DataUpgradesGameStats>();
            MeteorUpgrades = ResetSerializable<MeteorUpgrades>();
            Progression = ResetSerializable<Progression>();
            Settings = ResetSerializable<SettingsGameStats>();
            Timers = ResetSerializable<Timers>();
            TurretUpgrades = ResetSerializable<TurretUpgrades>();
            DataBankUpgrades = ResetSerializable<DataBankUpgrades>();
            DataBankOthers = ResetSerializable<DataBankOthers>();
        }
    }
}
