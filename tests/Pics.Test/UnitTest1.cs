using System;
using Xunit;

using Pics;

namespace Pics.Test
{
    public class UnitTest1
    {
        [Fact]
        public void TestForVersion()
        {
            var pic = new LocalPic();
            var actual = pic.Version();
            var expected = 1;
            Assert.Equal(expected, actual);
        }
    }
}
