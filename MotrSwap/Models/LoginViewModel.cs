using System.ComponentModel.DataAnnotations;

namespace MotrSwap.Models;

public class LoginViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } // Or email if you prefer

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    public bool RememberMe { get; set; }
}