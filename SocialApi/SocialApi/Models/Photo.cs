using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZwajApp.api.Models
{
    public class Photo
    {
        public int Id { get; set; }

        public string? Url { get; set; }

        public string? Description { get; set; }

        public DateTime DateAdded { get; set; }
        public bool IsMain { get; set; }

        public User? User { get; set; }

        [ForeignKey("User"), Required]
        public string? UserId { get; set; }
    }
}