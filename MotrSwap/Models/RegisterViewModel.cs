using System.ComponentModel.DataAnnotations;

namespace MotrSwap.Models;

public class RegisterViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    public string Username { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }
}