namespace ToggleSys.API.Models.Requests.Service
{
	public class UpdateSubjectRequest
	{
		public string Name { get; set; }
		public string Version { get; set; }
		public string SubscriptionEndpoint { get; set; }
	}
}