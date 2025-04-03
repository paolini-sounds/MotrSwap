using Microsoft.EntityFrameworkCore;
using MotrSwap.Models;

namespace MotrSwap.Services;

public class VehiclePostingService
{
    private readonly ApplicationDbContext _context;

    private string _defaultImageUrl =
        "https://res.cloudinary.com/dyen5x3uc/image/upload/v1743625864/MotrSwap/benjamin-child-7Cdw956mZ4w-unsplash_bthu5y.jpg";

    public VehiclePostingService(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<VehicleDisplayViewModel?> GetDisplayPostingByIdAsync(int id)
    {
        var posting = await _context.VehiclePostings
            .Include(p => p.Vehicle)
            .Include(p => p.User)
            .Include(p => p.VehicleImages)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (posting == null) return null;

        return new VehicleDisplayViewModel
        {
            Id = posting.Id,
            UserId = posting.User.Id,
            Year = posting.Vehicle.Year,
            Price = posting.Price,
            Mileage = posting.Vehicle.Mileage,
            Make = posting.Vehicle.Make,
            Description = posting.Description,
            Model = posting.Vehicle.Model,
            Username = posting.User.UserName,
            ImageUrl = posting.VehicleImages.FirstOrDefault()?.ImageUrl ?? _defaultImageUrl
        };
    }

    public async Task<List<VehicleDisplayViewModel>> GetAllDisplayPostingsAsync()
    {
        var postings = await _context.VehiclePostings
            .Include(p => p.Vehicle)
            .Include(p => p.User)
            .Include(p => p.VehicleImages)
            .ToListAsync();

        var result = postings.Select(p => new VehicleDisplayViewModel
        {
            Id = p.Id,
            UserId = p.User.Id,
            Year = p.Vehicle.Year,
            Make = p.Vehicle.Make,
            Model = p.Vehicle.Model,
            Mileage = p.Vehicle.Mileage,
            Price = p.Price,
            Email = p.User.Email ?? $"{p.User.UserName}@email.com",
            Username = p.User.UserName,
            PostedAt = p.PostedAt,
            ImageUrl = p.VehicleImages.FirstOrDefault()?.ImageUrl ??
                       _defaultImageUrl
        }).ToList();

        return result;
    }

    public async Task<int> CreatePostingAsync(VehiclePostingFormViewModel model, string userId)
    {
        var vehicle = new Vehicle
        {
            Make = model.Make,
            Model = model.VehicleModel,
            Year = model.Year,
            Mileage = model.Mileage
        };
        _context.Vehicles.Add(vehicle);
        await _context.SaveChangesAsync();
        
        var posting = new VehiclePosting
        {
            VehicleId = vehicle.Id,
            UserId = userId,
            Price = model.Price,
            Description = model.Description,
            PostedAt = DateTime.UtcNow,
            Status = "Active"
        };
        _context.VehiclePostings.Add(posting);
        await _context.SaveChangesAsync();

        return posting.Id;
    }
    
    public async Task<VehiclePostingFormViewModel?> GetPostingForEditAsync(int id)
    {
        var posting = await _context.VehiclePostings
            .Include(p => p.Vehicle)
            .Include(p => p.VehicleImages)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (posting == null) return null;

        return new VehiclePostingFormViewModel
        {
            Id = posting.Id,
            Year = posting.Vehicle.Year,
            Make = posting.Vehicle.Make,
            VehicleModel = posting.Vehicle.Model,
            Mileage = posting.Vehicle.Mileage,
            Price = posting.Price,
            Description = posting.Description
        };
    }

    public async Task<bool> UpdatePostingAsync(VehiclePostingFormViewModel model)
    {
        var posting = await _context.VehiclePostings
            .Include(p => p.Vehicle)
            .Include(p => p.VehicleImages)
            .FirstOrDefaultAsync(p => p.Id == model.Id);

        if (posting == null)
            return false;


        posting.Vehicle.Make = model.Make;
        posting.Vehicle.Model = model.VehicleModel;
        posting.Vehicle.Year = model.Year;
        posting.Vehicle.Mileage = model.Mileage;


        posting.Price = model.Price;
        posting.Description = model.Description;


        if (model.Image != null && model.Image.Length > 0)
        {

            var image = posting.VehicleImages.FirstOrDefault();
            if (image != null)
            {
                image.ImageUrl = "/images/sample.jpg";
                image.UploadedAt = DateTime.UtcNow;
            }
            else
            {
                _context.VehicleImages.Add(new VehicleImage
                {
                    VehiclePostingId = posting.Id,
                    ImageUrl = "/images/sample.jpg",
                    UploadedAt = DateTime.UtcNow
                });
            }
        }

        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> DeletePostingAsync(int id, string userId)
    {
        var posting = await _context.VehiclePostings
            .Include(p => p.Vehicle)
            .Include(p => p.VehicleImages)
            .FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);

        if (posting == null) return false;
        
        _context.VehicleImages.RemoveRange(posting.VehicleImages);
        
        _context.Vehicles.Remove(posting.Vehicle);
        _context.VehiclePostings.Remove(posting);

        await _context.SaveChangesAsync();
        return true;
    }
    
}