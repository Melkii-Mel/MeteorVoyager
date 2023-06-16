namespace MeteorVoyager.Assets.Scripts.Serialization.Interfaces
{
    /// <summary>
    /// Interface that allows to serialize objects using Serializer. Note that the object also should have an [Serializable] property
    /// </summary>
    public interface IMySerializable
    {
        string FileName { get; }
    }
}
