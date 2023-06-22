using SerializationLibrary;
using System.Collections.Generic;

namespace MeteorVoyager.Assets.Scripts.GameStatsNameSpace
{
    public class CurrentSave : Serializable<CurrentSave>
    {
        public string SaveName { get; set; }
        public int SaveIndex { get; set; }
        public List<string> SaveNames { get; set; } = new();
    }
}
