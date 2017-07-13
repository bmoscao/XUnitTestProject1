using SkillsWorkflow.IntegrationTests.Core;
using System;
using Xunit;

namespace SkillsWorkflow.IntegrationTests
{
    public class TimesheetBlockingTests
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
    }
}
