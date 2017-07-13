namespace SkillsWorkflow.IntegrationTests.Core
{
    public static class ApiHttpClientExtensions
	{
		public static HttpResponse ReadUserTypologyById(this ApiHttpClient client, string id)
		{
			return new HttpResponse(client.GetAsync($"api/usertypologies/{id}").Result);
		}
	}
}
