using System;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace SkillsWorkflow.IntegrationTests.Core
{
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
}
