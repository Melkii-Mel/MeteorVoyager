using System.Collections.Generic;
using SerializationLibrary;

namespace GameStatsNS
{
    public class CurrentSave : Serializable<CurrentSave>
    {
        public string SaveName { get; set; }
        public int SaveIndex { get; set; }
        public List<string> SaveNames { get; set; } = new();
    }
}
