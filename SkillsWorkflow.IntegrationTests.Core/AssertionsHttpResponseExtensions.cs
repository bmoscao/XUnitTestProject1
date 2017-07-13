using System.Net;
using Newtonsoft.Json.Linq;
using Xunit;

namespace SkillsWorkflow.IntegrationTests.Core
{
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
