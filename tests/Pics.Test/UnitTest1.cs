using System;
using NUnit.Framework;

using Pics;

namespace Pics.Test
{
    public class UnitTest1
    {
        [SetUp]
        public void Setup() {
        }

        [Test]
        public void TestForVersion()
        {
            var pic = new LocalPic();
            var actual = pic.Version();
            var expected = 1;
            Assert.AreEqual(expected, actual);
        }
    }
}
