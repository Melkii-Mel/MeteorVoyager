using MeteorVoyager.Assets.Scripts;
using NUnit.Framework;
using System;

namespace Assets.Scripts.UnitTests
{
    public class InfiniteIntegerTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void ATest1()
        {
            Assert.IsTrue(true);
        }

        [Test]
        public void Sum()
        {
            //sum
            Assert.That(new InfiniteInteger(100) + new InfiniteInteger(100) == 200, Is.True);
            Assert.That(new InfiniteInteger(100) + 100 == 200, Is.True);
            //sum small with large
            Assert.That(new InfiniteInteger(10000000) + new InfiniteInteger(1) == 10000001, Is.True);
            //sum large with small
            Assert.That(new InfiniteInteger(1) + new InfiniteInteger(10000000) == 10000001, Is.True);
        }
        [Test]
        public void Sub()
        {
            //subtraction
            Assert.That(new InfiniteInteger(10) - 9 == 1, Is.True);
            Assert.That(new InfiniteInteger(10) - new InfiniteInteger(9) == 1, Is.True);
        }
        [Test]
        public void Negatives()
        {
            Assert.That(new InfiniteInteger(-1) + new InfiniteInteger(-1) == -200, Is.False);
            Assert.That(new InfiniteInteger(-1) * 200 == -200, Is.True);
            Assert.That(new InfiniteInteger(100) - 315 == -215, Is.True);
            Assert.That(new InfiniteInteger(-100) * -100 == 10000, Is.True);
            Assert.That(new InfiniteInteger(-100) / -100 == 1, Is.True);
        }
        [Test]
        public void LargeMultiplication()
        {
            Assert.That(new InfiniteInteger(1, 100) * new InfiniteInteger(1, 200) == new InfiniteInteger(1, 300), Is.True);
        }
        [Test]
        public void LargeDivision()
        {
            Assert.That(new InfiniteInteger(1, 100) / new InfiniteInteger(1, 200) == new InfiniteInteger(0), Is.True);
            Assert.That(new InfiniteInteger(1, 100) / new InfiniteInteger(1, 101) == new InfiniteInteger(0.1), Is.True);
        }
        [Test]
        public void EverythingElse()
        {

            //multiplication
            Assert.That(new InfiniteInteger(90) * 9000 == 810000, Is.True);
            Assert.That(new InfiniteInteger(90) * new InfiniteInteger(9000) == 810000, Is.True);
            //division
            Assert.That(new InfiniteInteger(100) / 10 == 10, Is.True);
            Assert.That(new InfiniteInteger(100) / new InfiniteInteger(10) == 10, Is.True);
            //power
            InfiniteInteger powered = new InfiniteInteger(100).Pow(3);
            Assert.That(powered == 1000000, Is.True);
            //sqrt
            powered = new InfiniteInteger(100).Pow(0.5f);
            Assert.That(powered == 10, Is.True);
        }
        [Test]
        public void RandomTests()
        {
            Random random = new();
            for (int i = 0; i < 1000; i++)
            {
                int v1 = random.Next(1, 1000000);
                int v2 = random.Next(1, 1000000);
                InfiniteInteger i1 = v1;
                InfiniteInteger i2 = v2;
                Assert.That(i1 + i2 == v1 + v2, Is.True);
            }
            for (int i = 0; i < 1000; i++)
            {
                int v1 = random.Next(1, 1000000);
                int v2 = random.Next(1, v1);
                InfiniteInteger i1 = v1;
                InfiniteInteger i2 = v2;
                Assert.That(i1 - i2 == v1 - v2, Is.True);
            }
            for (int i = 0; i < 1000; i++)
            {
                int v1 = random.Next(1, 1000);
                int v2 = random.Next(1, 1000);
                InfiniteInteger i1 = v1;
                InfiniteInteger i2 = v2;
                Assert.That(i1 * i2 == v1 * v2, Is.True);
            }
            for (int i = 0; i < 1000; i++)
            {
                int v1 = random.Next(1, 1000000);
                int v2 = random.Next(1, 1000000);
                InfiniteInteger i1 = v1;
                InfiniteInteger i2 = v2;
                Assert.That(i1 / i2 == (double)v1 / v2, Is.True);
            }
        }
        [Test]
        public void NewPowTests()
        {
            Assert.That(InfiniteInteger.Pow(10, 3) == InfiniteInteger.OldPow(10, 3), Is.True);
            int ii = (int)InfiniteInteger.Pow(5, 5);
            int integer = (int)MathF.Pow(5, 5);
            Assert.That(ii == integer, Is.True);
        }
        [Test]
        public void PerformanceTestsNewPow()
        {
            for (int i = 0; i < 100000; i++)
            {
                Assert.That(InfiniteInteger.Pow(11, 9) == 2357947691, Is.True);
            }
        }
        [Test]
        [Obsolete]
        public void PerformanceTestsOldPow()
        {
            for (int i = 0; i < 100000; i++)
            {
                Assert.That(InfiniteInteger.OldPow(11, 9) == 2357947691, Is.True);
            }
        }
    }
}