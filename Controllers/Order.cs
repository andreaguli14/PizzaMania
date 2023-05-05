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

[Authorize(Roles = "Manager")]
public class OrderController : Controller
{ 

    private UserManager<User> _user;
    private ApplicationDbContext _db;
    public OrderController(ApplicationDbContext db, UserManager<User> user)
    {
        _db = db;
        _user = user;
    }

    public IActionResult Index()
    {
      var model = _db.Orders.Include(x=>x.CartObjects).ThenInclude(x => x.Pizza).ThenInclude(x=>x.Ingredients).ToList();
          return View(model);
    }

    public IActionResult UpdateStatus(int id, string status)
    {
        var model = _db.Orders.FirstOrDefault(x => x.Id == id);
        model.Status = status;
        _db.SaveChanges();
        return RedirectToAction("Index");
    }

}

