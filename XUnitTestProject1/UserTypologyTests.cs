using SkillsWorkflow.IntegrationTests.Core;
using Xunit;

namespace SkillsWorkflow.IntegrationTests
{
    public class UserTypologyTests
    {
        [Fact]
        public void ReadExistingUserTypologyTest()
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
