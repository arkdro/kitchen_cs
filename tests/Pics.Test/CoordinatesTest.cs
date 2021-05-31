using System;
using NUnit.Framework;

using Pics;

namespace Pics.Test
{
    public class CoordinatesTest
    {

        [SetUp]
        public void Setup() {
        }

        [Test]
        public void EqualsTest()
        {
            var c1 = new Coordinates(x: 1234, y: 5432);
            var c2 = new Coordinates(x: 1234, y: 5432);
            var actual = c1.Equals(c2);
            var expected = true;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void EqualityTest()
        {
            var c1 = new Coordinates(x: 1234, y: 5432);
            var c2 = new Coordinates(x: 1234, y: 5432);
            var actual = c1 == c2;
            var expected = true;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void InequalityTest()
        {
            var c1 = new Coordinates(x: 1234, y: 5432);
            var c2 = new Coordinates(x: 1234, y: 5432);
            var actual = c1 != c2;
            var expected = false;
            Assert.AreEqual(expected, actual);
        }
    }
}
