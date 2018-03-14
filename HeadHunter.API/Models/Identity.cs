using System.ComponentModel.DataAnnotations;

namespace HeadHunter.API.Models
{
    public class Identity
    {
        [Key]
        public int Id { get; set; }
    }
}