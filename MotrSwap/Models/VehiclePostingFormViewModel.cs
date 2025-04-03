using System.ComponentModel.DataAnnotations;

namespace MotrSwap.Models;

public class VehiclePostingFormViewModel
{
    public int Id { get; set; }
    [Required]
    public int Year { get; set; }
    [Required]
    public string Make { get; set; }
    [Required]
    public string VehicleModel { get; set; }
    [Required]
    public int Mileage { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required]
    public string Description { get; set; }
    
    public IFormFile? Image { get; set; }
}