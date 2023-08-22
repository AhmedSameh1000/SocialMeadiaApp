using System.ComponentModel.DataAnnotations;

namespace ZwajApp.api.ViewModels;

public class RegisterModel
{
    [Required]
    public string? Email { get; set; }
    [Required]
    public string? UserName { get; set; }

    [Required]
    public string? Password { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required]
    public string? Gender { get; set; }
}
