using System.Collections.Generic;

namespace HeadHunter.API.Models
{
    public class Subject
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        //Unique
        public string Email { get; set; }
        public string ProfilePhoto { get; set; }
        public SocialContact SocialContact { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
    }
}