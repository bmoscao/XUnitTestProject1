using System;
using Xunit;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        

        [Fact]
        public void Test1()
        {
            var client = ClientFactory.CreateApiClient();
            client.LoginWithUser("John", "12345");
            client.ReadUserTypologyById("8C618DD3-84B1-4116-A6ED-0DF9998422BD")
                  .AssertThatExists()
                  .AssertFieldIsEqualTo("Name", "Account")
                  .AssertFieldIsEqualToCaseInsensitive("Id", "8C618DD3-84B1-4116-A6ED-0DF9998422BD");
        }

        [Fact]
        public void FailingTest1()
        {
            Assert.True(false);
        }
    }
}
