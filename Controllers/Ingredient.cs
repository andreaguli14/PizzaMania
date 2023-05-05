using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PizzaMania.Models;
using PizzaMania.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace PizzaMania.Controllers;

[Authorize(Roles = "Manager")]
public class IngredientController : Controller
{
    
    private ApplicationDbContext _db;
    public IngredientController(ApplicationDbContext db)
    {
        
        _db = db;
    }

    public IActionResult Index()
    {
      var model = _db.Ingredients.ToList();
          return View(model);
    }

    public IActionResult AddIngredient()
    {
        
        return View();
    }

    [HttpPost]
    public IActionResult AddIngredient(Ingredient model)
    {

        _db.Ingredients.Add(model);
        _db.SaveChanges();

        return RedirectToAction("Index");
    }


}

