using System;
using Xunit;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Assert.InRange(5, 1, 10);
        }

        [Fact]
        public void FailingTest1()
        {
            Assert.True(false);
        }
    }
}
