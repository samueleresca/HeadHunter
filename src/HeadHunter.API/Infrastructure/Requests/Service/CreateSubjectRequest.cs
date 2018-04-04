namespace HeadHunter.API.Infrastructure.Requests.Service
{
	public class CreateSubjectRequest
	{
	    public string Name { get; set; }
	    public string Surname { get; set; }
	    public string Phone { get; set; }
	    //Unique
	    public string Email { get; set; }
    }
}