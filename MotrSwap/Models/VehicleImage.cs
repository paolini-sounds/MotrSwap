using System.ComponentModel.DataAnnotations;

namespace MotrSwap.Models;

public class VehicleImage
{
    public int Id { get; set; }
    
    //foreign key to VehiclePosting
    public int VehiclePostingId { get; set; }
    public VehiclePosting VehiclePosting { get; set; }
    
    public string ImageUrl { get; set; }
    public DateTime UploadedAt { get; set; }
    
}