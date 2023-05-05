using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PizzaMania.Models;
using PizzaMania.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace PizzaMania.Controllers;

[Authorize]
public class PizzaController : Controller
{
    private IWebHostEnvironment _host;
    private UserManager<User> _user;
    private ApplicationDbContext _db;
    public PizzaController(ApplicationDbContext db,  IWebHostEnvironment hostEnvironment, UserManager<User> user)
    {
        _host = hostEnvironment;
        _db = db;
        _user = user;
    }

    [Authorize(Roles = "Manager")]
    public IActionResult Index()
    {
      var model = _db.Pizzas.Include(x => x.Ingredients).ToList();
          return View(model);
    }

    [Authorize(Roles = "Manager")]
    public IActionResult AddPizza()
    {
        
        return View();
    }

    [Authorize(Roles = "Manager")]
    [HttpPost]
    public IActionResult AddPizza(AddPizza model)
    {
        if (model.Ingredients.Any())
        {
            foreach (var i in model.Ingredients)
            {
                var ingre = _db.Ingredients.FirstOrDefault(x => x.Id == i);
                model.Pizza.Ingredients.Add(ingre);
            }
        }

        if (model.Pizza.formFile != null)
        {
                string wwwRootPath = _host.WebRootPath;
                string filename = Path.GetFileNameWithoutExtension(model.Pizza.formFile.FileName);
                string extension = Path.GetExtension(model.Pizza.formFile.FileName);
                filename = filename + DateTime.Now.ToString("ddMMyyyyHHmmss") + extension;
                string path = Path.Combine(wwwRootPath + "/img/", filename);
                string pathsave = "/img/" + filename;
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    model.Pizza.formFile.CopyTo(fileStream);
                }
            model.Pizza.Photo = pathsave;
        }
        _db.Pizzas.Add(model.Pizza);
        _db.SaveChanges();

        return RedirectToAction("Index");
    }

    [AllowAnonymous]
    public IActionResult PizzaDetails(int id)
    {
        var model = _db.Pizzas.Include(x => x.Ingredients).FirstOrDefault(x=>x.Id == id);
        return View(model);
    }



    public IActionResult AddToCart(int id, int quantity)
    {


        var oggetto = new CartObjects();
        oggetto.Pizza = _db.Pizzas.FirstOrDefault(x => x.Id == id);
        oggetto.Quantity = quantity;

        var user = _db.Users.Include(x => x.cart)
                     .ThenInclude(x => x.CartObjects)
                     .ThenInclude(x => x.Pizza)
                     .ThenInclude(x => x.Ingredients)
                     .FirstOrDefault(x => x.Id == _user.GetUserId(User));

        user.cart.CartObjects.Add(oggetto);
        _db.SaveChanges();

        return RedirectToAction("Index");

    }

    [AllowAnonymous]
    public IActionResult Store()
    {
        var model = _db.Pizzas.Include(x => x.Ingredients).ToList();
        return View(model);
    }



}

