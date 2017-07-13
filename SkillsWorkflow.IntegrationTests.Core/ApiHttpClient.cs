using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace SkillsWorkflow.IntegrationTests.Core
{
    public class ApiHttpClient : HttpClient
	{
        public LoginResultDto LoginWithUser(string userName, string password)
        {
            var content = new StringContent(JsonConvert.SerializeObject(new { UserName = userName, Password = password, Persistent = true }), Encoding.UTF8, "application/json");
            var request = PostAsync("api/auth/token", content).Result;

            request.EnsureSuccessStatusCode();

            var response = request.Content.ReadAsStringAsync().Result;
            var loginResult = JsonConvert.DeserializeObject<LoginResultDto>(response);

            DefaultRequestHeaders.Remove("X-AccessToken");
            DefaultRequestHeaders.Add("X-AccessToken", loginResult.AccessToken);

            return loginResult;
        }
    }
}
