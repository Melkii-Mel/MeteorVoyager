using NUnit.Framework;
using SerializationLibrary;
using UnityEngine;

namespace UnitTests
{
    public class SerializationTests
    {
        [Test]
        public void Test1()
        {
            TestHolder testHolder = new()
            {
                TestSerializable =
                {
                    StringFieldTest = "Ten",
                    IntegerFieldTest = 10,
                    InfiniteIntegerFieldTest = (10, 10)
                }
            };
            testHolder.SerializeAll();

            TestHolder testHolder2 = new();
            Assert.AreEqual(10, testHolder2.TestSerializable.IntegerFieldTest);
            Assert.AreEqual("Ten", testHolder2.TestSerializable.StringFieldTest);
            Assert.AreEqual(new InfiniteInteger(10, 10), testHolder2.TestSerializable.InfiniteIntegerFieldTest);
        }
    }

    public class TestHolder : SerializablesHolder
    {
        public TestSerializable TestSerializable { get; set; }
        public TestHolder() : base(0, Application.isPlaying, 1, Application.persistentDataPath)
        {
            TestSerializable = CreateSerializable<TestSerializable>();
        }
    }

    public class TestSerializable : Serializable<TestSerializable>
    {
        public int IntegerFieldTest { get; set; }
        public string StringFieldTest { get; set; } = "Zero";
        public InfiniteInteger InfiniteIntegerFieldTest { get; set; } = (0, 0);
    }
}
