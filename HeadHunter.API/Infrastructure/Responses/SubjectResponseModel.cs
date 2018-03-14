using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HeadHunter.API.Models;

namespace ToggleSys.API.Models.Responses
{
	public class SubjectResponseModel
	{
	    [Key]
	    public int Id { get; set; }
	    public string Name { get; set; }
	    public string Surname { get; set; }
	    public string Phone { get; set; }
	    public string Email { get; set; }
	    public string ProfilePhoto { get; set; }
	    public SocialContact SocialContact { get; set; }
	    public IEnumerable<Feedback> Feedbacks { get; set; }
    }
}