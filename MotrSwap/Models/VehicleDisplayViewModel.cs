namespace MotrSwap.Models;

public class VehicleDisplayViewModel
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public int Year { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public int Mileage { get; set; }
    public string ImageUrl { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public DateTime PostedAt { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
}