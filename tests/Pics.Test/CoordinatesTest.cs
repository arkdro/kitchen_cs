using System;
using Xunit;

using Pics;

namespace Pics.Test
{
    public class CoordinatesTest
    {
        [Fact]
        public void EqualsTest()
        {
            var c1 = new Coordinates(x: 1234, y: 5432);
            var c2 = new Coordinates(x: 1234, y: 5432);
            var actual = c1.Equals(c2);
            var expected = true;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void EqualityTest()
        {
            var c1 = new Coordinates(x: 1234, y: 5432);
            var c2 = new Coordinates(x: 1234, y: 5432);
            var actual = c1 == c2;
            var expected = true;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void InequalityTest()
        {
            var c1 = new Coordinates(x: 1234, y: 5432);
            var c2 = new Coordinates(x: 1234, y: 5432);
            var actual = c1 != c2;
            var expected = false;
            Assert.Equal(expected, actual);
        }
    }
}
