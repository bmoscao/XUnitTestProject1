using System;
using System.Net.Http.Headers;

namespace SkillsWorkflow.IntegrationTests.Core
{
    public static class ClientFactory
	{
		//public static IntegrationHttpClient CreateIntegrationClient()
		//{
		//	var client = new IntegrationHttpClient();
		//	client.DefaultRequestHeaders.Accept.Clear();
		//	client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		//	client.DefaultRequestHeaders.Add("X-AppId", Settings.AppId);
		//	client.DefaultRequestHeaders.Add("X-AppSecret", Settings.AppSecret);
		//	client.BaseAddress = new Uri(Settings.IntegrationApiBaseUrl);
		//	return client;
		//}

		public static ApiHttpClient CreateApiClient()
		{
			var client = new ApiHttpClient();
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			//client.DefaultRequestHeaders.Add("X-AppId", Settings.AppId);
			//client.DefaultRequestHeaders.Add("X-AppSecret", Settings.AppSecret);
			client.BaseAddress = new Uri(Settings.ApiBaseUrl);
			return client;
		}
	}
}
