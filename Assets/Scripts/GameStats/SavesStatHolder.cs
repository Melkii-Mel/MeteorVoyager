using SerializationLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MeteorVoyager.Assets.Scripts.GameStatsNameSpace
{
    public class SavesStatHolder : SerializablesHolder
    {
        public CurrentSave Save { get; set; }
        public SavesStatHolder(Func<bool> isRunning, float delayS) : base(0, isRunning, delayS, Application.persistentDataPath)
        {
            Save = CreateSerializable<CurrentSave>();
        }
    }
}
