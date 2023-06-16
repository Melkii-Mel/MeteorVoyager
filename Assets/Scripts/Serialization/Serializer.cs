using MeteorVoyager.Assets.Scripts.Serialization.Interfaces;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace MeteorVoyager.Assets.Scripts.Serialization
{
    public class Serializer
    {
        public static void Serialize(IMySerializable item)
        {
            PerformSerializationDeserializationLogic(FileMode.Create, item);
        }
        public static void Deserialize(IMySerializable item)
        {
            PerformSerializationDeserializationLogic(FileMode.Open, item);
        }
        private static void PerformSerializationDeserializationLogic(FileMode mode, IMySerializable item)
        {
            try
            {
                string pathToFile = Path.Combine(Application.persistentDataPath, item.FileName);
                using FileStream fileStream = new(pathToFile, mode);
                BinaryFormatter formatter = new();
                if (mode == FileMode.Create)
                {
                    formatter.Serialize(fileStream, item);
                }
                else
                {
                    item = (IMySerializable)formatter.Deserialize(fileStream);
                }
            }
            catch (Exception)
            {
                CreateFile(item);
                PerformSerializationDeserializationLogic(mode, item);
            }
        }
        private static void CreateFile(IMySerializable item)
        {
            string pathToFile = Path.Combine(Application.persistentDataPath, item.FileName);
            using FileStream fileStream = new(pathToFile, FileMode.Create);
            BinaryFormatter formatter = new();
            formatter.Serialize(fileStream, item);
        }
    }
}
