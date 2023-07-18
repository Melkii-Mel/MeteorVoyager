using System;
using SerializationLibrary;
using UnityEngine;

namespace GameStatsNS
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
