namespace HeadHunter.API.Infrastructure.Requests.Service
{
	public class UpdateSubjectRequest
	{
		public string Name { get; set; }
		public string Version { get; set; }
		public string SubscriptionEndpoint { get; set; }
	}
}