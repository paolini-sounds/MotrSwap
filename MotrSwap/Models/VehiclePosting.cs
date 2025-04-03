using System.ComponentModel.DataAnnotations;

namespace MotrSwap.Models;

public class VehiclePosting
{
    public int Id { get; set; }
    
    //foreign key to vehicle
    public int VehicleId { get; set; }
    public Vehicle Vehicle { get; set; }
    
    //foreign key to user
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
    
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public DateTime PostedAt { get; set; } = DateTime.Now;
    
    public string Status { get; set; }
    
    public ICollection<VehicleImage> VehicleImages { get; set; }
}