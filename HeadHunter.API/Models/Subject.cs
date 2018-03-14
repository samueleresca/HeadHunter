using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HeadHunter.API.Models
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        //Unique
        public string Email { get; set; }
        public string ProfilePhoto { get; set; }
        public SocialContact SocialContact { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
        public bool IsDeleted { get; set; }
    }
}