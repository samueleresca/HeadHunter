using System.ComponentModel.DataAnnotations;

namespace HeadHunter.API.Models
{
    public class SocialContact
    {
        [Key]
        public int Id { get; set; }
        public string Linkedin { get; set; }
        public string Twitter { get; set; }
        public string Facebook { get; set; }
    }
}