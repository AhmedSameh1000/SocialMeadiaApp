using System.ComponentModel.DataAnnotations;

namespace ZwajApp.api.ViewModels
{
    public class UserViewModel
    {
        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}