using Microsoft.AspNetCore.Identity;

namespace MotrSwap.Models;

public class ApplicationUser : IdentityUser
{
    public DateTime CreatedAt { get; set; }
    public ICollection<VehiclePosting>? VehiclePostings { get; set; }
}