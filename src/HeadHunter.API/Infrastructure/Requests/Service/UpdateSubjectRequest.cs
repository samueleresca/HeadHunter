using HeadHunter.API.Models;

namespace HeadHunter.API.Infrastructure.Requests.Service
{
	public class UpdateSubjectRequest
	{
	    public string Name { get; set; }
	    public string Surname { get; set; }
	    public string Phone { get; set; }
	    //Unique
	    public string Email { get; set; }
	    public string ProfilePhoto { get; set; }
	    public SocialContact SocialContact { get; set; }
    }
}