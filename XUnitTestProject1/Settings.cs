using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace XUnitTestProject1
{
	public static class Settings
	{
        //public static string AppId => ConfigurationManager.AppSettings["Settings:AppId"]; // "E501FD1E-754E-4AB1-A9D8-8A0CAA424990";
        //public static string AppSecret => ConfigurationManager.AppSettings["Settings:AppSecret"]; // "1VY9r3SGnCj+qCGELT9wugnDjNAn1g7usK4roe+Pigg=";
        //public static string IntegrationApiBaseUrl => ConfigurationManager.AppSettings["Settings:IntegrationApiBaseUrl"]; // "http://localhost:28635/";
        public static string ApiBaseUrl => "https://apiv2-playground-dev-we.skillsworkflow.com"; // ConfigurationManager.AppSettings["Settings:ApiBaseUrl"]; // "http://localhost:30449/";
	}

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
	
    public class LoginResultDto
	{
		public bool IsAdministrator { get; set; }
		public string AccessToken { get; set; }
		public string CompanyId { get; set; }
		public string UserId { get; set; }
		public string UserName { get; set; }
		public bool IsClient { get; set; }
		public DateTime TokenExpirationUtc { get; set; }
		public string[] Roles { get; set; }
	}

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

	public class HttpResponse
	{
		private JObject _jsonContent;

		public HttpResponseMessage Response { get; }

		public JObject JsonContent
		{
			get
			{
				if (_jsonContent == null)
				{
                    var j = Response.Content.ReadAsStringAsync().Result;
                    _jsonContent = JObject.Parse(j);
					//_jsonContent = Response.Content.ReadAsAsync<JObject>().Result;
				}
				return _jsonContent;
			}
		}

		public HttpResponse(HttpResponseMessage response)
		{
			if (response == null) throw new ArgumentNullException(nameof(response));
			Response = response;
		}
	}

	public static class ApiHtpClientExtensions
	{
		public static HttpResponse ReadUserTypologyById(this ApiHttpClient client, string id)
		{
			return new HttpResponse(client.GetAsync($"api/usertypologies/{id}").Result);
		}
	}

	public static class AssertionsHttpResponseExtensions
	{
		public static HttpResponse AssertRequestIsInvalid(this HttpResponse response)
		{
			Assert.Equal(HttpStatusCode.BadRequest, response.Response.StatusCode);
			return response;
		}

		public static HttpResponse AssertFieldIsEqualTo<T>(this HttpResponse response, string fieldName, T expectedValue)
		{
			Assert.NotEqual(HttpStatusCode.InternalServerError, response.Response.StatusCode);
			var responseJson = response.JsonContent;
			Assert.Equal(expectedValue, responseJson[fieldName].Value<T>());
			return response;
		}

		public static HttpResponse AssertFieldIsEqualToCaseInsensitive(this HttpResponse response, string fieldName, string expectedValue)
		{
			Assert.NotEqual(HttpStatusCode.InternalServerError, response.Response.StatusCode);
			var responseJson = response.JsonContent;
			Assert.Equal(expectedValue.ToLowerInvariant(), responseJson[fieldName].Value<string>().ToLowerInvariant());
			return response;
		}

		public static HttpResponse AssertFieldIsPresent(this HttpResponse response, string fieldName)
		{
			Assert.NotEqual(HttpStatusCode.InternalServerError, response.Response.StatusCode);
			var responseJson = response.JsonContent;
			Assert.NotNull(responseJson[fieldName]);
			return response;
		}

		public static HttpResponse AssertThatExists(this HttpResponse response)
		{
			Assert.Equal(HttpStatusCode.OK, response.Response.StatusCode);
			return response;
		}

		public static HttpResponse ExtractField<T>(this HttpResponse response, string fieldName, out T value)
		{
			response.AssertFieldIsPresent(fieldName);
			var responseJson = response.JsonContent;
			value = responseJson[fieldName].Value<T>();
			return response;
		}
	}
}
