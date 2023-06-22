using SerializationLibrary;
using System;
using System.Collections.Generic;

namespace MeteorVoyager.Assets.Scripts.GameStatsNameSpace
{
    public class DataUpgradesGameStats : Serializable<DataUpgradesGameStats>
    {
        public int BossSpawnChanceLvl { get; set; } = 0;
        public int ForceFieldLvl { get; set; } = 0;
        public int UltracoinSpawnChanceLvl { get; set; } = 0;
        public int ScreenExplosionLvl { get; set; } = 0;
        public int MultishotLvl { get; set; } = 0;

        public enum Upgrades
        {
            BossSpawnChanceLvl,
            ForceFieldLvl,
            UltracoinSpawnChanceLvl,
            ScreenExplosionLvl,
            MultishotLvl,
        }
        public static List<Func<int, InfiniteInteger>> Functions { get; }
    }
}
