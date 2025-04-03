using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotrSwap.Models;
using MotrSwap.Services;

namespace MotrSwap.Controllers;

public class VehicleController : Controller
{
    private readonly VehiclePostingService _postingService;
    private readonly UserManager<ApplicationUser> _userManager;

    public VehicleController(VehiclePostingService postingService, UserManager<ApplicationUser> userManager)
    {
        _postingService = postingService;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var listings = await _postingService.GetAllDisplayPostingsAsync();
        return View(listings);
    }

    public async Task<IActionResult> Details(int id)
    {
        var posting = await _postingService.GetDisplayPostingByIdAsync(id);
        if (posting == null) return NotFound();
        return View(posting);
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        return View("CreateEdit", new VehiclePostingFormViewModel());
    }
    
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var model = await _postingService.GetPostingForEditAsync(id);
        if (model == null) return NotFound();

        return View("CreateEdit", model); // reuse the same form view
    }

    [HttpPost]
    public async Task<IActionResult> Edit(VehiclePostingFormViewModel model)
    {
        if (!ModelState.IsValid)
            return View("CreateEdit", model);

        var success = await _postingService.UpdatePostingAsync(model);

        if (!success)
            return NotFound();

        return RedirectToAction("Details", new { id = model.Id });
    }
    

    [HttpPost]
    public async Task<IActionResult> Create(VehiclePostingFormViewModel model)
    {
        Console.WriteLine($"Make: {model.Make}");
        Console.WriteLine($"Model: {model.VehicleModel}");
        Console.WriteLine($"Description: {model.Description}");
        
        
        if (!ModelState.IsValid)
        {
            foreach (var entry in ModelState)
            {
                foreach (var error in entry.Value.Errors)
                {
                    Console.WriteLine($"Field: {entry.Key}, Error: {error.ErrorMessage}");
                }
            }

            return View("CreateEdit",model);
        }

        var userId = _userManager.GetUserId(User);

        var postingId = await _postingService.CreatePostingAsync(model, userId);
        
        return RedirectToAction("Details", new { id = postingId });
    }
    
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = _userManager.GetUserId(User);
        var success = await _postingService.DeletePostingAsync(id, userId);
        if (!success) return Forbid();

        return RedirectToAction("Index");
    }
}