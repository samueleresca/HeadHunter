using System;
using System.ComponentModel.DataAnnotations;

namespace HeadHunter.API.Models
{
    public class Feedback
    {

        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public Identity Identity { get; set; }
    }
}