using System;

namespace SkillsWorkflow.IntegrationTests.Core
{
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
}
