using MeteorVoyager.Assets.Scripts.GameStatsNameSpace;
using SerializationLibrary;
using System;
using UnityEngine;

namespace MeteorVoyager.Assets.Scripts.GameStatsNameSpace
{
    public class GameStatsHolder : SerializablesHolder
    {
        public Currency Currency { get; set; }
        public DataUpgradesGameStats DataUpgrades { get; set; }
        public MeteorUpgrades MeteorUpgrades { get; set; }
        public Progression Progression { get; set; }
        public SettingsGameStats Settings { get; set; }
        public Timers Timers { get; set; }
        public TurretUpgrades TurretUpgrades { get; set; }
        public GameStatsHolder(int serializablesIndex, Func<bool> runningCondition, float delayS) : base(serializablesIndex, runningCondition, delayS, Application.persistentDataPath)
        {
            Currency = CreateSerializable<Currency>();
            DataUpgrades = CreateSerializable<DataUpgradesGameStats>();
            MeteorUpgrades = CreateSerializable<MeteorUpgrades>();
            Progression = CreateSerializable<Progression>();
            Settings = CreateSerializable<SettingsGameStats>();
            Timers = CreateSerializable<Timers>();
            TurretUpgrades = CreateSerializable<TurretUpgrades>();
        }
    }
}
